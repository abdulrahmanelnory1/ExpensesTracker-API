using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class BudgetCategoryAndUserBudgetRelationsship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryID",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryID",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Budgets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID1",
                table: "Budgets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryID",
                table: "Budgets",
                column: "CategoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryID1",
                table: "Budgets",
                column: "CategoryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryID",
                table: "Budgets",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryID1",
                table: "Budgets",
                column: "CategoryID1",
                principalTable: "Categories",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryID1",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryID",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_CategoryID1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "CategoryID1",
                table: "Budgets");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "Budgets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryID",
                table: "Budgets",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryID",
                table: "Budgets",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
