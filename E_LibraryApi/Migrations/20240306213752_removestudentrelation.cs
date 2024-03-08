using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class removestudentrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_Student_StudentId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_StudentId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "BorrowedBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "BorrowedBooks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_StudentId",
                table: "BorrowedBooks",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_Student_StudentId",
                table: "BorrowedBooks",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
