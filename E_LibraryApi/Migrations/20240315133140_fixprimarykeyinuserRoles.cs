using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_LibraryApi.Migrations
{
    /// <inheritdoc />
    public partial class fixprimarykeyinuserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersRole_Role",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersRole",
                table: "UsersRole");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UsersRole",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "UsersRole",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRole",
                table: "UsersRole",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersRole_Role",
                table: "Users",
                column: "Role",
                principalTable: "UsersRole",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UsersRole_Role",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersRole",
                table: "UsersRole");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "UsersRole",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UsersRole",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRole",
                table: "UsersRole",
                column: "Role");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UsersRole_Role",
                table: "Users",
                column: "Role",
                principalTable: "UsersRole",
                principalColumn: "Role",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
