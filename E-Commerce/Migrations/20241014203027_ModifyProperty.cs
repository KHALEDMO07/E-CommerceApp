using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "CartProducts");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "CartProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartProducts");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "CartProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
