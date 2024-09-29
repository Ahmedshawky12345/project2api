using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project2api.Migrations
{
    /// <inheritdoc />
    public partial class cartandrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeeTasks_Products_emp_id",
                table: "employeeTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_employeeTasks_tasks_task_id",
                table: "employeeTasks");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.RenameColumn(
                name: "emp_id",
                table: "employeeTasks",
                newName: "cart_id");

            migrationBuilder.RenameColumn(
                name: "task_id",
                table: "employeeTasks",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_employeeTasks_emp_id",
                table: "employeeTasks",
                newName: "IX_employeeTasks_cart_id");

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_employeeTasks_Carts_cart_id",
                table: "employeeTasks",
                column: "cart_id",
                principalTable: "Carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employeeTasks_Orders_order_id",
                table: "employeeTasks",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeeTasks_Carts_cart_id",
                table: "employeeTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_employeeTasks_Orders_order_id",
                table: "employeeTasks");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "employeeTasks",
                newName: "emp_id");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "employeeTasks",
                newName: "task_id");

            migrationBuilder.RenameIndex(
                name: "IX_employeeTasks_cart_id",
                table: "employeeTasks",
                newName: "IX_employeeTasks_emp_id");

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_employeeTasks_Products_emp_id",
                table: "employeeTasks",
                column: "emp_id",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employeeTasks_tasks_task_id",
                table: "employeeTasks",
                column: "task_id",
                principalTable: "tasks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
