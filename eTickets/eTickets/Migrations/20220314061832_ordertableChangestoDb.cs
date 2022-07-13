using Microsoft.EntityFrameworkCore.Migrations;

namespace eTickets.Migrations
{
    public partial class ordertableChangestoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Register_RoleName_RNid",
                table: "Register");

            migrationBuilder.DropIndex(
                name: "IX_Register_RNid",
                table: "Register");

            migrationBuilder.DropColumn(
                name: "RNid",
                table: "Register");

            migrationBuilder.RenameColumn(
                name: "RName",
                table: "Register",
                newName: "RoleName");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Register",
                newName: "RName");

            migrationBuilder.AddColumn<int>(
                name: "RNid",
                table: "Register",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Register_RNid",
                table: "Register",
                column: "RNid");

            migrationBuilder.AddForeignKey(
                name: "FK_Register_RoleName_RNid",
                table: "Register",
                column: "RNid",
                principalTable: "RoleName",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
