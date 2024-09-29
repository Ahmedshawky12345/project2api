using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project2api.Migrations
{
    /// <inheritdoc />
    public partial class done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_categories_dept_id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "dept_id",
                table: "Products",
                newName: "cat_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_dept_id",
                table: "Products",
                newName: "IX_Products_cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_categories_cat_id",
                table: "Products",
                column: "cat_id",
                principalTable: "categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_categories_cat_id",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "cat_id",
                table: "Products",
                newName: "dept_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_cat_id",
                table: "Products",
                newName: "IX_Products_dept_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_categories_dept_id",
                table: "Products",
                column: "dept_id",
                principalTable: "categories",
                principalColumn: "Id");
        }
    }
}
