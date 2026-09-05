using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m009_AddPreviousVersionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_records_RecordId",
                table: "record_versions");

            migrationBuilder.RenameColumn(
                name: "RecordId",
                table: "record_versions",
                newName: "record_id");

            migrationBuilder.RenameIndex(
                name: "IX_record_versions_RecordId",
                table: "record_versions",
                newName: "IX_record_versions_record_id");

            migrationBuilder.AddColumn<Guid>(
                name: "previous_version_id",
                table: "theme_versions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "previous_version_id",
                table: "theme_field_versions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "previous_version_id",
                table: "record_versions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "previous_version_id",
                table: "record_value_versions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_theme_versions_previous_version_id",
                table: "theme_versions",
                column: "previous_version_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_field_versions_previous_version_id",
                table: "theme_field_versions",
                column: "previous_version_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_versions_previous_version_id",
                table: "record_versions",
                column: "previous_version_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_value_versions_previous_version_id",
                table: "record_value_versions",
                column: "previous_version_id");

            migrationBuilder.AddForeignKey(
                name: "FK_record_value_versions_record_value_versions_previous_versio~",
                table: "record_value_versions",
                column: "previous_version_id",
                principalTable: "record_value_versions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_record_versions_previous_version_id",
                table: "record_versions",
                column: "previous_version_id",
                principalTable: "record_versions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_records_record_id",
                table: "record_versions",
                column: "record_id",
                principalTable: "records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_field_versions_theme_field_versions_previous_version_~",
                table: "theme_field_versions",
                column: "previous_version_id",
                principalTable: "theme_field_versions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_theme_versions_theme_versions_previous_version_id",
                table: "theme_versions",
                column: "previous_version_id",
                principalTable: "theme_versions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_value_versions_record_value_versions_previous_versio~",
                table: "record_value_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_record_versions_previous_version_id",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_records_record_id",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_field_versions_theme_field_versions_previous_version_~",
                table: "theme_field_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_versions_theme_versions_previous_version_id",
                table: "theme_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_versions_previous_version_id",
                table: "theme_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_field_versions_previous_version_id",
                table: "theme_field_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_versions_previous_version_id",
                table: "record_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_value_versions_previous_version_id",
                table: "record_value_versions");

            migrationBuilder.DropColumn(
                name: "previous_version_id",
                table: "theme_versions");

            migrationBuilder.DropColumn(
                name: "previous_version_id",
                table: "theme_field_versions");

            migrationBuilder.DropColumn(
                name: "previous_version_id",
                table: "record_versions");

            migrationBuilder.DropColumn(
                name: "previous_version_id",
                table: "record_value_versions");

            migrationBuilder.RenameColumn(
                name: "record_id",
                table: "record_versions",
                newName: "RecordId");

            migrationBuilder.RenameIndex(
                name: "IX_record_versions_record_id",
                table: "record_versions",
                newName: "IX_record_versions_RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_records_RecordId",
                table: "record_versions",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
