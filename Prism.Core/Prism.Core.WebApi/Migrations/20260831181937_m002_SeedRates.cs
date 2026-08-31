using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prism.Core.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class m002_SeedRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "rates",
                columns: new[] { "id", "color", "letter", "value" },
                values: new object[,]
                {
                    { new Guid("13524847-c476-41d1-8834-84db11b4a05d"), -16776961, 'E', 4 },
                    { new Guid("17818146-13fe-4569-b59a-c82828e550bf"), -23296, 'A', 9 },
                    { new Guid("180ae0be-d8dd-415c-b1bb-d1ca81543187"), -7077677, 'F', 2 },
                    { new Guid("34a688e2-472b-41aa-b9b0-fa1331b2c5fe"), -7077677, 'F', 1 },
                    { new Guid("3500e2f4-16f0-486f-b75f-147687573ab8"), -16744448, 'C', 6 },
                    { new Guid("57c0097b-a044-41d4-a6a7-8b500f7cf08d"), -256, 'B', 7 },
                    { new Guid("5a606102-34ec-4b30-89aa-47e1bdccfea6"), -16711681, 'D', 5 },
                    { new Guid("7d3bdd20-b87c-4344-abe3-30dd224fb0db"), -65536, 'S', 10 },
                    { new Guid("a757745b-c04a-4479-aca4-5c64ff8625d4"), -23296, 'A', 8 },
                    { new Guid("f1229048-2cd3-4b74-87a8-90062776a481"), -7077677, 'F', 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("13524847-c476-41d1-8834-84db11b4a05d"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("17818146-13fe-4569-b59a-c82828e550bf"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("180ae0be-d8dd-415c-b1bb-d1ca81543187"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("34a688e2-472b-41aa-b9b0-fa1331b2c5fe"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("3500e2f4-16f0-486f-b75f-147687573ab8"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("57c0097b-a044-41d4-a6a7-8b500f7cf08d"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("5a606102-34ec-4b30-89aa-47e1bdccfea6"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("7d3bdd20-b87c-4344-abe3-30dd224fb0db"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("a757745b-c04a-4479-aca4-5c64ff8625d4"));

            migrationBuilder.DeleteData(
                table: "rates",
                keyColumn: "id",
                keyValue: new Guid("f1229048-2cd3-4b74-87a8-90062776a481"));
        }
    }
}
