using FileTrader.API.Controllers;
using FileTrader.AppServices.UserFiles.Repositories;
using FileTrader.AppServices.UserFiles.Services;
using FileTrader.AppServices.Users.Repositories;
using FileTrader.AppServices.Users.Services;
using FileTrader.Contracts.UserFiles;
using FileTrader.Contracts.Users;
using FileTrader.DataAccess;
using FileTrader.DataAccess.UserFiles.Repository;
using FileTrader.DataAccess.Users.Repository;
using FileTrader.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FileTrader.ComponentRegistrar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using FileTrader.API;
using FileTrader.Domain;
using Microsoft.AspNetCore.Identity;
using FileTrader.AppServices.Auth.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API дл€ документов", Version = "v1" });
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(UserController).Assembly.GetName().Name}.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(UserDTO).Assembly.GetName().Name}.xml")));
});
builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7002") // ”кажите адрес вашего клиента
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddServices();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddTransient<IUserFilesService, UserFilesService>();
builder.Services.AddScoped<IUserFilesRepository, UserFilesRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());

//builder.Services.AddIdentity<FileTrader.Domain.IdentityUserApp, IdentityRole>()
//    .AddDefaultTokenProviders().AddUserStore<ApplicationDbContext>().AddRoleStore<ApplicationDbContext>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;
//});


builder.Services.AddSwaggerModule();





builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        //options.SaveToken = false;
        //options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
