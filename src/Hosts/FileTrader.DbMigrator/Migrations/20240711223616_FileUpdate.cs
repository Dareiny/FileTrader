using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileTrader.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class FileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GeneralAccess",
                table: "UserFiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralAccess",
                table: "UserFiles");
        }
    }
}
