using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseWeb.Migrations
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expenseAppUserId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_expenseAppUserId",
                table: "Expenses",
                column: "expenseAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_expenseAppUserId",
                table: "Expenses",
                column: "expenseAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_expenseAppUserId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_expenseAppUserId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "expenseAppUserId",
                table: "Expenses");
        }
    }
}
