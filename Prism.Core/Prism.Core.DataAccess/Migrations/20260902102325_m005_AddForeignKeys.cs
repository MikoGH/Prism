using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m005_AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_rates_rate_id",
                table: "user_rates",
                column: "rate_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_rates_record_id",
                table: "user_rates",
                column: "record_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_rates_user_id",
                table: "user_rates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_theme_fields_theme_id",
                table: "theme_fields",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_records_theme_id",
                table: "records",
                column: "theme_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_values_record_id",
                table: "record_values",
                column: "record_id");

            migrationBuilder.CreateIndex(
                name: "IX_record_values_theme_field_id",
                table: "record_values",
                column: "theme_field_id");

            migrationBuilder.AddForeignKey(
                name: "FK_record_values_records_record_id",
                table: "record_values",
                column: "record_id",
                principalTable: "records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_record_values_theme_fields_theme_field_id",
                table: "record_values",
                column: "theme_field_id",
                principalTable: "theme_fields",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_records_themes_theme_id",
                table: "records",
                column: "theme_id",
                principalTable: "themes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_theme_fields_themes_theme_id",
                table: "theme_fields",
                column: "theme_id",
                principalTable: "themes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_rates_rates_rate_id",
                table: "user_rates",
                column: "rate_id",
                principalTable: "rates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_rates_records_record_id",
                table: "user_rates",
                column: "record_id",
                principalTable: "records",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_rates_users_user_id",
                table: "user_rates",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_record_values_records_record_id",
                table: "record_values");

            migrationBuilder.DropForeignKey(
                name: "FK_record_values_theme_fields_theme_field_id",
                table: "record_values");

            migrationBuilder.DropForeignKey(
                name: "FK_records_themes_theme_id",
                table: "records");

            migrationBuilder.DropForeignKey(
                name: "FK_theme_fields_themes_theme_id",
                table: "theme_fields");

            migrationBuilder.DropForeignKey(
                name: "FK_user_rates_rates_rate_id",
                table: "user_rates");

            migrationBuilder.DropForeignKey(
                name: "FK_user_rates_records_record_id",
                table: "user_rates");

            migrationBuilder.DropForeignKey(
                name: "FK_user_rates_users_user_id",
                table: "user_rates");

            migrationBuilder.DropIndex(
                name: "IX_user_rates_rate_id",
                table: "user_rates");

            migrationBuilder.DropIndex(
                name: "IX_user_rates_record_id",
                table: "user_rates");

            migrationBuilder.DropIndex(
                name: "IX_user_rates_user_id",
                table: "user_rates");

            migrationBuilder.DropIndex(
                name: "IX_theme_fields_theme_id",
                table: "theme_fields");

            migrationBuilder.DropIndex(
                name: "IX_records_theme_id",
                table: "records");

            migrationBuilder.DropIndex(
                name: "IX_record_values_record_id",
                table: "record_values");

            migrationBuilder.DropIndex(
                name: "IX_record_values_theme_field_id",
                table: "record_values");
        }
    }
}
