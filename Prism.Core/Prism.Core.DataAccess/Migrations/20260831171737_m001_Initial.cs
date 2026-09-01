using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prism.Core.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m001_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false),
                    letter = table.Column<char>(type: "character(1)", nullable: false),
                    color = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "record_value_versions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    record_value_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    write_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_accepted = table.Column<bool>(type: "boolean", nullable: false),
                    int_value = table.Column<int>(type: "integer", nullable: true),
                    double_value = table.Column<double>(type: "double precision", nullable: true),
                    string_value = table.Column<string>(type: "text", nullable: true),
                    bool_value = table.Column<bool>(type: "boolean", nullable: true),
                    date_value = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_record_value_versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "record_values",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    record_id = table.Column<Guid>(type: "uuid", nullable: false),
                    theme_field_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_record_values", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "record_versions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    write_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_accepted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_record_versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    theme_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "theme_field_versions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    theme_field_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    write_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_accepted = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme_field_versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "theme_fields",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    theme_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme_fields", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "theme_versions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    theme_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    write_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_accepted = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theme_versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "themes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_themes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_rates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    record_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rate_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_rates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rates");

            migrationBuilder.DropTable(
                name: "record_value_versions");

            migrationBuilder.DropTable(
                name: "record_values");

            migrationBuilder.DropTable(
                name: "record_versions");

            migrationBuilder.DropTable(
                name: "records");

            migrationBuilder.DropTable(
                name: "theme_field_versions");

            migrationBuilder.DropTable(
                name: "theme_fields");

            migrationBuilder.DropTable(
                name: "theme_versions");

            migrationBuilder.DropTable(
                name: "themes");

            migrationBuilder.DropTable(
                name: "user_rates");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
