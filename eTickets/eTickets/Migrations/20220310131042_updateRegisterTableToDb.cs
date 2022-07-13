using Microsoft.EntityFrameworkCore.Migrations;

namespace eTickets.Migrations
{
    public partial class updateRegisterTableToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Register");

            migrationBuilder.AddColumn<int>(
                name: "RNid",
                table: "Register",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "Register",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
