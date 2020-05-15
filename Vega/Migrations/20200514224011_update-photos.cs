using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Migrations
{
    public partial class updatephotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "VehiclesPhotos",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "VehiclesPhotos",
                newName: "Location");
        }
    }
}
