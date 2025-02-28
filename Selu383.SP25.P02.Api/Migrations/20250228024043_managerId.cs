using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Selu383.SP25.P02.Api.Migrations
{
    /// <inheritdoc />
    public partial class managerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_AspNetUsers_ManagerId",
                table: "Theaters");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Theaters",
                newName: "managerId");

            migrationBuilder.RenameIndex(
                name: "IX_Theaters_ManagerId",
                table: "Theaters",
                newName: "IX_Theaters_managerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_AspNetUsers_managerId",
                table: "Theaters",
                column: "managerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_AspNetUsers_managerId",
                table: "Theaters");

            migrationBuilder.RenameColumn(
                name: "managerId",
                table: "Theaters",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Theaters_managerId",
                table: "Theaters",
                newName: "IX_Theaters_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_AspNetUsers_ManagerId",
                table: "Theaters",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
