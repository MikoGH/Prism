using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m004_AddUserCreateAndUserAccept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "theme_versions",
                newName: "user_create_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "theme_field_versions",
                newName: "user_create_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "record_versions",
                newName: "user_create_id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "record_value_versions",
                newName: "user_create_id");

            migrationBuilder.AddColumn<Guid>(
                name: "user_accept_id",
                table: "theme_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_accept_id",
                table: "theme_field_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_accept_id",
                table: "record_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_accept_id",
                table: "record_value_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_theme_versions_theme_id",
                table: "theme_versions",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_versions_user_accept_id",
                table: "theme_versions",
                column: "user_accept_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_versions_user_create_id",
                table: "theme_versions",
                column: "user_create_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_field_versions_theme_field_id",
                table: "theme_field_versions",
                column: "theme_field_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_field_versions_user_accept_id",
                table: "theme_field_versions",
                column: "user_accept_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_field_versions_user_create_id",
                table: "theme_field_versions",
                column: "user_create_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_versions_RecordId",
                table: "record_versions",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_record_versions_user_accept_id",
                table: "record_versions",
                column: "user_accept_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_versions_user_create_id",
                table: "record_versions",
                column: "user_create_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_value_versions_record_value_id",
                table: "record_value_versions",
                column: "record_value_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_value_versions_user_accept_id",
                table: "record_value_versions",
                column: "user_accept_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_value_versions_user_create_id",
                table: "record_value_versions",
                column: "user_create_id");

            migrationBuilder.AddForeignKey(
                name: "FK_record_value_versions_record_values_record_value_id",
                table: "record_value_versions",
                column: "record_value_id",
                principalTable: "record_values",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_record_value_versions_users_user_accept_id",
                table: "record_value_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_record_value_versions_users_user_create_id",
                table: "record_value_versions",
                column: "user_create_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_records_RecordId",
                table: "record_versions",
                column: "RecordId",
                principalTable: "records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_users_user_accept_id",
                table: "record_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_users_user_create_id",
                table: "record_versions",
                column: "user_create_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_field_versions_theme_fields_theme_field_id",
                table: "theme_field_versions",
                column: "theme_field_id",
                principalTable: "theme_fields",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_field_versions_users_user_accept_id",
                table: "theme_field_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_field_versions_users_user_create_id",
                table: "theme_field_versions",
                column: "user_create_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_versions_themes_theme_id",
                table: "theme_versions",
                column: "theme_id",
                principalTable: "themes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_versions_users_user_accept_id",
                table: "theme_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_versions_users_user_create_id",
                table: "theme_versions",
                column: "user_create_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_value_versions_record_values_record_value_id",
                table: "record_value_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_value_versions_users_user_accept_id",
                table: "record_value_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_value_versions_users_user_create_id",
                table: "record_value_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_records_RecordId",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_users_user_accept_id",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_users_user_create_id",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_field_versions_theme_fields_theme_field_id",
                table: "theme_field_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_field_versions_users_user_accept_id",
                table: "theme_field_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_field_versions_users_user_create_id",
                table: "theme_field_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_versions_themes_theme_id",
                table: "theme_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_versions_users_user_accept_id",
                table: "theme_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_versions_users_user_create_id",
                table: "theme_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_versions_theme_id",
                table: "theme_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_versions_user_accept_id",
                table: "theme_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_versions_user_create_id",
                table: "theme_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_field_versions_theme_field_id",
                table: "theme_field_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_field_versions_user_accept_id",
                table: "theme_field_versions");

            migrationBuilder.DropIndex(
                name: "IX_theme_field_versions_user_create_id",
                table: "theme_field_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_versions_RecordId",
                table: "record_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_versions_user_accept_id",
                table: "record_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_versions_user_create_id",
                table: "record_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_value_versions_record_value_id",
                table: "record_value_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_value_versions_user_accept_id",
                table: "record_value_versions");

            migrationBuilder.DropIndex(
                name: "IX_record_value_versions_user_create_id",
                table: "record_value_versions");

            migrationBuilder.DropColumn(
                name: "user_accept_id",
                table: "theme_versions");

            migrationBuilder.DropColumn(
                name: "user_accept_id",
                table: "theme_field_versions");

            migrationBuilder.DropColumn(
                name: "user_accept_id",
                table: "record_versions");

            migrationBuilder.DropColumn(
                name: "user_accept_id",
                table: "record_value_versions");

            migrationBuilder.RenameColumn(
                name: "user_create_id",
                table: "theme_versions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "user_create_id",
                table: "theme_field_versions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "user_create_id",
                table: "record_versions",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "user_create_id",
                table: "record_value_versions",
                newName: "user_id");
        }
    }
}
