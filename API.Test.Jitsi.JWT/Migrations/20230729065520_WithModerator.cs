using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Test.Jitsi.JWT.Migrations
{
    /// <inheritdoc />
    public partial class WithModerator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isModerator",
                table: "UserModels",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isModerator",
                table: "UserModels");
        }
    }
}
