using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAcrossUkraine.WebApi.Migrations
{
    public partial class UpdatedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "GivenName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "GivenName",
                table: "Users",
                newName: "FirstName");
        }
    }
}
