using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class EditRegularExpenseDBSetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecurringExpenses_Expenses_ID",
                table: "RecurringExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringExpenses",
                table: "RecurringExpenses");

            migrationBuilder.RenameTable(
                name: "RecurringExpenses",
                newName: "RegularExpenses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegularExpenses",
                table: "RegularExpenses",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularExpenses_Expenses_ID",
                table: "RegularExpenses",
                column: "ID",
                principalTable: "Expenses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularExpenses_Expenses_ID",
                table: "RegularExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegularExpenses",
                table: "RegularExpenses");

            migrationBuilder.RenameTable(
                name: "RegularExpenses",
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
    }
}
