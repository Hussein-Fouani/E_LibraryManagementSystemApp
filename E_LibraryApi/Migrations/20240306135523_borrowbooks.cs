using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class borrowbooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_User_UserId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "BookPurchaseDate",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookQuantity",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "BorrowedBooks",
                newName: "BorrowDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BorrowedBooks",
                newName: "BorrowBookId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Book",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Book",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Users_UserId",
                table: "BorrowedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Users_UserId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "BorrowDate",
                table: "BorrowedBooks",
                newName: "IssueDate");

            migrationBuilder.RenameColumn(
                name: "BorrowBookId",
                table: "BorrowedBooks",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "BorrowedBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookPurchaseDate",
                table: "Book",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BookQuantity",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_User_UserId",
                table: "BorrowedBooks",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
