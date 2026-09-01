using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m003_AddIsDeletedToThemeVersionAndThemeFieldVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "theme_fields");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "theme_versions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "theme_field_versions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "theme_versions");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "theme_field_versions");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "theme_fields",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
