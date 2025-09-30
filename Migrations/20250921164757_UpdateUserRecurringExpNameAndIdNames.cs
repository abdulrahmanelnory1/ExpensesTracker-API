using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRecurringExpNameAndIdNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserID",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserID",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Expenses_ExpensesID",
                table: "ExpenseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Items_ItemsID",
                table: "ExpenseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserID",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryID",
                table: "Items");

            migrationBuilder.DropTable(
                name: "RegularExpenses");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Items",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Items",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryID",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Expenses",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_UserID",
                table: "Expenses",
                newName: "IX_Expenses_UserId");

            migrationBuilder.RenameColumn(
                name: "ItemsID",
                table: "ExpenseItem",
                newName: "ItemsId");

            migrationBuilder.RenameColumn(
                name: "ExpensesID",
                table: "ExpenseItem",
                newName: "ExpensesId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseItem_ItemsID",
                table: "ExpenseItem",
                newName: "IX_ExpenseItem_ItemsId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Categories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UserID",
                table: "Categories",
                newName: "IX_Categories_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Budgets",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Budgets",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Budgets",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_UserID",
                table: "Budgets",
                newName: "IX_Budgets_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_CategoryID",
                table: "Budgets",
                newName: "IX_Budgets_CategoryId");

            migrationBuilder.CreateTable(
                name: "RecurringExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NextDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    LastProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringExpenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringExpenses_Expenses_Id",
                        column: x => x.Id,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Expenses_ExpensesId",
                table: "ExpenseItem",
                column: "ExpensesId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Items_ItemsId",
                table: "ExpenseItem",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Categories_CategoryId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Expenses_ExpensesId",
                table: "ExpenseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseItem_Items_ItemsId",
                table: "ExpenseItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "RecurringExpenses");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Items",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Items",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                newName: "IX_Items_CategoryID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Expenses",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expenses",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                newName: "IX_Expenses_UserID");

            migrationBuilder.RenameColumn(
                name: "ItemsId",
                table: "ExpenseItem",
                newName: "ItemsID");

            migrationBuilder.RenameColumn(
                name: "ExpensesId",
                table: "ExpenseItem",
                newName: "ExpensesID");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseItem_ItemsId",
                table: "ExpenseItem",
                newName: "IX_ExpenseItem_ItemsID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Categories",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                newName: "IX_Categories_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Budgets",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Budgets",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Budgets",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                newName: "IX_Budgets_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                newName: "IX_Budgets_CategoryID");

            migrationBuilder.CreateTable(
                name: "RegularExpenses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextDueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularExpenses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RegularExpenses_Expenses_ID",
                        column: x => x.ID,
                        principalTable: "Expenses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Categories_CategoryID",
                table: "Budgets",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserID",
                table: "Budgets",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserID",
                table: "Categories",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Expenses_ExpensesID",
                table: "ExpenseItem",
                column: "ExpensesID",
                principalTable: "Expenses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseItem_Items_ItemsID",
                table: "ExpenseItem",
                column: "ItemsID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserID",
                table: "Expenses",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryID",
                table: "Items",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
