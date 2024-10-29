using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotskinWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductAddPricePerKg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PricePerKg",
                table: "Products",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerKg",
                table: "Products");
        }
    }
}
