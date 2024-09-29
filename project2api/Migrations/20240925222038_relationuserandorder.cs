using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project2api.Migrations
{
    /// <inheritdoc />
    public partial class relationuserandorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userid",
                table: "Orders",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_userid",
                table: "Orders",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_userid",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_userid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "Orders");
        }
    }
}
