using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudySpot.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlaceTypesAndPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlaceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Library" },
                    { 2, "Cafe" },
                    { 3, "University" },
                    { 4, "Home" },
                    { 5, "Other" }
                });

            migrationBuilder.InsertData(
                table: "StudyPlaces",
                columns: new[] { "Id", "HasPowerOutlets", "HasWifi", "Location", "Name", "Notes", "PlaceTypeId" },
                values: new object[,]
                {
                    { 1, true, true, "Central Square, second floor", "City Library Reading Room", "Quiet at all times. Lockers at the entrance.", 1 },
                    { 2, false, true, "Park Street 12", "Corner Cafe", "Good for short sessions. Gets loud around noon.", 2 },
                    { 3, true, true, "Main campus, building B, room 204", "Faculty Study Hall", null, 3 },
                    { 4, true, true, "Home office", "Desk at Home", "Best for evening sessions.", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlaceTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StudyPlaces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudyPlaces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudyPlaces",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudyPlaces",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PlaceTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlaceTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlaceTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlaceTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
