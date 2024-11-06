using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotskinWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModelRenamePerKg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PricePerKg",
                table: "Products",
                newName: "PerKg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PerKg",
                table: "Products",
                newName: "PricePerKg");
        }
    }
}
