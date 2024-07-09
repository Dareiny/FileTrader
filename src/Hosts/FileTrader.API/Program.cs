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

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddTransient<IUserFilesService, UserFilesService>();
builder.Services.AddScoped<IUserFilesRepository, UserFilesRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());


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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
