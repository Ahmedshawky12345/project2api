using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project2api.Migrations
{
    /// <inheritdoc />
    public partial class someshacnges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "cartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cartItems_productid",
                table: "cartItems",
                column: "productid");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_Products_productid",
                table: "cartItems",
                column: "productid",
                principalTable: "Products",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_Products_productid",
                table: "cartItems");

            migrationBuilder.DropIndex(
                name: "IX_cartItems_productid",
                table: "cartItems");

            migrationBuilder.DropColumn(
                name: "productid",
                table: "cartItems");
        }
    }
}
