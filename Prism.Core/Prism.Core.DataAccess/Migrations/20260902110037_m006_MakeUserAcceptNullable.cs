using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m006_MakeUserAcceptNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_value_versions_users_user_accept_id",
                table: "record_value_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_users_user_accept_id",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_field_versions_users_user_accept_id",
                table: "theme_field_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_versions_users_user_accept_id",
                table: "theme_versions");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "theme_versions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "theme_field_versions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "record_versions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "record_value_versions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_record_value_versions_users_user_accept_id",
                table: "record_value_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_record_versions_users_user_accept_id",
                table: "record_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_theme_field_versions_users_user_accept_id",
                table: "theme_field_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_theme_versions_users_user_accept_id",
                table: "theme_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_value_versions_users_user_accept_id",
                table: "record_value_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_record_versions_users_user_accept_id",
                table: "record_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_field_versions_users_user_accept_id",
                table: "theme_field_versions");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_versions_users_user_accept_id",
                table: "theme_versions");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "theme_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "theme_field_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "record_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_accept_id",
                table: "record_value_versions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_record_value_versions_users_user_accept_id",
                table: "record_value_versions",
                column: "user_accept_id",
                principalTable: "users",
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
                name: "FK_theme_field_versions_users_user_accept_id",
                table: "theme_field_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_versions_users_user_accept_id",
                table: "theme_versions",
                column: "user_accept_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
