using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Test.Jitsi.JWT.Migrations
{
    /// <inheritdoc />
    public partial class OnlyAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "AppID",
                table: "UserModels");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UserModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "UserModels",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AppID",
                table: "UserModels",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UserModels",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
