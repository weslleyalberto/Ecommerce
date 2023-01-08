using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRODUTO_AspNetUsers_ApplicationUsersId",
                table: "TB_PRODUTO");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUsersId",
                table: "TB_PRODUTO",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PRD_URL",
                table: "TB_PRODUTO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PRODUTO_AspNetUsers_ApplicationUsersId",
                table: "TB_PRODUTO",
                column: "ApplicationUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRODUTO_AspNetUsers_ApplicationUsersId",
                table: "TB_PRODUTO");

            migrationBuilder.DropColumn(
                name: "PRD_URL",
                table: "TB_PRODUTO");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUsersId",
                table: "TB_PRODUTO",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PRODUTO_AspNetUsers_ApplicationUsersId",
                table: "TB_PRODUTO",
                column: "ApplicationUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
