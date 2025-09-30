using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class CreateRegularExpenseDBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularExpense_Expenses_ID",
                table: "RegularExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegularExpense",
                table: "RegularExpense");

            migrationBuilder.RenameTable(
                name: "RegularExpense",
                newName: "RecurringExpenses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringExpenses",
                table: "RecurringExpenses",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringExpenses_Expenses_ID",
                table: "RecurringExpenses",
                column: "ID",
                principalTable: "Expenses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringExpenses_Expenses_ID",
                table: "RecurringExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringExpenses",
                table: "RecurringExpenses");

            migrationBuilder.RenameTable(
                name: "RecurringExpenses",
                newName: "RegularExpense");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegularExpense",
                table: "RegularExpense",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularExpense_Expenses_ID",
                table: "RegularExpense",
                column: "ID",
                principalTable: "Expenses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
