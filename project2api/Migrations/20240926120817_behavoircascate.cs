using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project2api.Migrations
{
    /// <inheritdoc />
    public partial class behavoircascate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_categories_cat_id",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_categories_cat_id",
                table: "Products",
                column: "cat_id",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_categories_cat_id",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_categories_cat_id",
                table: "Products",
                column: "cat_id",
                principalTable: "categories",
                principalColumn: "Id");
        }
    }
}
