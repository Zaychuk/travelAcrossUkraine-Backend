using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace travelAcrossUkraine.WebApi.Migrations
{
    public partial class AddedGeoPointsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoordinateX = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    CoordinateY = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoPoints", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoPoints");
        }
    }
}
