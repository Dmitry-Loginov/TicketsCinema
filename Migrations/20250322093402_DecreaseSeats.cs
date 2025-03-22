using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsCinema.Migrations
{
    /// <inheritdoc />
    public partial class DecreaseSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Seats",
                keyColumn: "Id",
                keyValue: 214);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Seats",
                column: "Id",
                values: new object[]
                {
                    213,
                    214
                });
        }
    }
}
