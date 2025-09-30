using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemExpenseRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItem");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ExpenseId",
                table: "Items",
                column: "ExpenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Expenses_ExpenseId",
                table: "Items",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Expenses_ExpenseId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ExpenseId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "ExpenseItem",
                columns: table => new
                {
                    ExpensesId = table.Column<int>(type: "int", nullable: false),
                    ItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItem", x => new { x.ExpensesId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_ExpenseItem_Expenses_ExpensesId",
                        column: x => x.ExpensesId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseItem_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItem_ItemsId",
                table: "ExpenseItem",
                column: "ItemsId");
        }
    }
}
