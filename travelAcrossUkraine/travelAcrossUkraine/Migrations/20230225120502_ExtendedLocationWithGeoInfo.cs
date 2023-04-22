using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAcrossUkraine.WebApi.Migrations
{
    public partial class ExtendedLocationWithGeoInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CircleId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GeoPointId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PolygonId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CircleId",
                table: "Locations",
                column: "CircleId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_GeoPointId",
                table: "Locations",
                column: "GeoPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_PolygonId",
                table: "Locations",
                column: "PolygonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Circles_CircleId",
                table: "Locations",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_GeoPoints_GeoPointId",
                table: "Locations",
                column: "GeoPointId",
                principalTable: "GeoPoints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Polygons_PolygonId",
                table: "Locations",
                column: "PolygonId",
                principalTable: "Polygons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Circles_CircleId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_GeoPoints_GeoPointId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Polygons_PolygonId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_CircleId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_GeoPointId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_PolygonId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CircleId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GeoPointId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "PolygonId",
                table: "Locations");
        }
    }
}
