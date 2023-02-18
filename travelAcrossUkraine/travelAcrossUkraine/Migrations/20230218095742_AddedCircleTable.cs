using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAcrossUkraine.WebApi.Migrations
{
    public partial class AddedCircleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Circles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterGeoPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Radius = table.Column<decimal>(type: "decimal(20,10)", precision: 20, scale: 10, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Circles_GeoPoints_CenterGeoPointId",
                        column: x => x.CenterGeoPointId,
                        principalTable: "GeoPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Circles_CenterGeoPointId",
                table: "Circles",
                column: "CenterGeoPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circles");
        }
    }
}
