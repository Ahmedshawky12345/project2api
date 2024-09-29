using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project2api.Migrations
{
    /// <inheritdoc />
    public partial class rleationbetweenorderitemandproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "orderItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_productId",
                table: "orderItems",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_Products_productId",
                table: "orderItems",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_Products_productId",
                table: "orderItems");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_productId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "orderItems");
        }
    }
}
