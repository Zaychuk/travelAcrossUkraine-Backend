using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAcrossUkraine.WebApi.Migrations
{
    public partial class AddedPolygonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PolygonId",
                table: "GeoPoints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "GeoPoints",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "GeoPoints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Polygons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polygons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoPoints_PolygonId",
                table: "GeoPoints",
                column: "PolygonId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoPoints_Polygons_PolygonId",
                table: "GeoPoints",
                column: "PolygonId",
                principalTable: "Polygons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoPoints_Polygons_PolygonId",
                table: "GeoPoints");

            migrationBuilder.DropTable(
                name: "Polygons");

            migrationBuilder.DropIndex(
                name: "IX_GeoPoints_PolygonId",
                table: "GeoPoints");

            migrationBuilder.DropColumn(
                name: "PolygonId",
                table: "GeoPoints");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "GeoPoints");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "GeoPoints");
        }
    }
}
