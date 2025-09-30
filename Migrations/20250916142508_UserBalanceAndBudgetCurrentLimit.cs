using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class UserBalanceAndBudgetCurrentLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryID1",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryID1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CategoryID1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Spent",
                table: "Budgets");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentLimit",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentLimit",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID1",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Spent",
                table: "Budgets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryID1",
                table: "Budgets",
                column: "CategoryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryID1",
                table: "Budgets",
                column: "CategoryID1",
                principalTable: "Categories",
                principalColumn: "ID");
        }
    }
}
