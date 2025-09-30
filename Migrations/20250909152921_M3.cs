using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesTracker.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Expenses_Users_UserID",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ExpenseId",
                table: "Items");

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
                name: "Id",
                table: "Expenses",
                newName: "ID");

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
                name: "Id",
                table: "Budgets",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ID",
                table: "Expenses",
                newName: "Id");

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
                name: "ID",
                table: "Budgets",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ExpenseId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Expenses_Users_UserID",
                table: "Expenses",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
