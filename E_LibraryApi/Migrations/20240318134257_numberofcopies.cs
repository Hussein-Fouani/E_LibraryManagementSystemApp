using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class numberofcopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfCopies",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfCopies",
                table: "Book");
        }
    }
}
