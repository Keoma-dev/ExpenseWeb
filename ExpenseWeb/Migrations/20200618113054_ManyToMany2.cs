using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseWeb.Migrations
{
    public partial class ManyToMany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpense_Expenses_ExpenseId",
                table: "PersonExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpense_Person_PersonId",
                table: "PersonExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonExpense",
                table: "PersonExpense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "PersonExpense",
                newName: "PersonExpenses");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Persons");

            migrationBuilder.RenameIndex(
                name: "IX_PersonExpense_PersonId",
                table: "PersonExpenses",
                newName: "IX_PersonExpenses_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonExpenses",
                table: "PersonExpenses",
                columns: new[] { "ExpenseId", "PersonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpenses_Expenses_ExpenseId",
                table: "PersonExpenses",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpenses_Persons_PersonId",
                table: "PersonExpenses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpenses_Expenses_ExpenseId",
                table: "PersonExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonExpenses_Persons_PersonId",
                table: "PersonExpenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonExpenses",
                table: "PersonExpenses");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "Person");

            migrationBuilder.RenameTable(
                name: "PersonExpenses",
                newName: "PersonExpense");

            migrationBuilder.RenameIndex(
                name: "IX_PersonExpenses_PersonId",
                table: "PersonExpense",
                newName: "IX_PersonExpense_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonExpense",
                table: "PersonExpense",
                columns: new[] { "ExpenseId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpense_Expenses_ExpenseId",
                table: "PersonExpense",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonExpense_Person_PersonId",
                table: "PersonExpense",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
