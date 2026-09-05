using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m008_AddIsChecked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_checked",
                table: "theme_versions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_checked",
                table: "theme_field_versions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_checked",
                table: "record_versions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_checked",
                table: "record_value_versions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_checked",
                table: "theme_versions");

            migrationBuilder.DropColumn(
                name: "is_checked",
                table: "theme_field_versions");

            migrationBuilder.DropColumn(
                name: "is_checked",
                table: "record_versions");

            migrationBuilder.DropColumn(
                name: "is_checked",
                table: "record_value_versions");
        }
    }
}
