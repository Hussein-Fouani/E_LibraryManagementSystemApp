using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class createSignInModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SignIn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignIn", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SignIn",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { new Guid("e6cb49e3-6905-4800-a6ca-a9b645ab18ef"), "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignIn");
        }
    }
}
