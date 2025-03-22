using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsCinema.Migrations
{
    /// <inheritdoc />
    public partial class DecreaseSeats2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 212);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Seats",
                column: "Id",
                values: new object[]
                {
                    211,
                    212
                });
        }
    }
}
