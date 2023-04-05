using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Casino.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePropertyFromGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
