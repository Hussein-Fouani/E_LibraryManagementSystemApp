using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class changeconstraintonbookstocascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Book_BookId",
                table: "BorrowedBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Book_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Book_BookId",
                table: "BorrowedBooks");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Book_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
