using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class addbookmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BookAuthor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BookPublication = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BookPrice = table.Column<double>(type: "float", nullable: false),
                    BookPurhcaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
