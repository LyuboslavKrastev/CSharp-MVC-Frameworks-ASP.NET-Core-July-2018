using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibrary.App.Migrations
{
    public partial class BorrowedBooksDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowersBooks_Books_BookId",
                table: "BorrowersBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowersBooks_Borrowers_BorrowerId",
                table: "BorrowersBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowersBooks",
                table: "BorrowersBooks");

            migrationBuilder.RenameTable(
                name: "BorrowersBooks",
                newName: "BorrowedBooks");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowersBooks_BookId",
                table: "BorrowedBooks",
                newName: "IX_BorrowedBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks",
                columns: new[] { "BorrowerId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Borrowers_BorrowerId",
                table: "BorrowedBooks",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Books_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Borrowers_BorrowerId",
                table: "BorrowedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowedBooks",
                table: "BorrowedBooks");

            migrationBuilder.RenameTable(
                name: "BorrowedBooks",
                newName: "BorrowersBooks");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowersBooks",
                newName: "IX_BorrowersBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowersBooks",
                table: "BorrowersBooks",
                columns: new[] { "BorrowerId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowersBooks_Books_BookId",
                table: "BorrowersBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowersBooks_Borrowers_BorrowerId",
                table: "BorrowersBooks",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
