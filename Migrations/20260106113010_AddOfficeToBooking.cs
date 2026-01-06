using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddOfficeToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OfficeId",
                table: "Bookings",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Offices_OfficeId",
                table: "Bookings",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Offices_OfficeId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_OfficeId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Bookings");
        }
    }
}
