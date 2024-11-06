using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotskinWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModelAddAmountInStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountInStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountInStock",
                table: "Products");
        }
    }
}
