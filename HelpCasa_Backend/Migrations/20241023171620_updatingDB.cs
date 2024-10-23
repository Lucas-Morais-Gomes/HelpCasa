using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpCasa_Backend.Migrations
{
    /// <inheritdoc />
    public partial class updatingDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_EmployeeId",
                table: "Servicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicos_Usuarios_EmployerId",
                table: "Servicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Servicos",
                table: "Servicos");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Servicos",
                newName: "Services");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_EmployerId",
                table: "Services",
                newName: "IX_Services_EmployerId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicos_EmployeeId",
                table: "Services",
                newName: "IX_Services_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Experience",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_EmployerId",
                table: "Services",
                column: "EmployerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_EmployeeId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_EmployerId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Servicos");

            migrationBuilder.RenameIndex(
                name: "IX_Services_EmployerId",
                table: "Servicos",
                newName: "IX_Servicos_EmployerId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_EmployeeId",
                table: "Servicos",
                newName: "IX_Servicos_EmployeeId");

            migrationBuilder.AlterColumn<List<string>>(
                name: "Experience",
                table: "Usuarios",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Servicos",
                table: "Servicos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_EmployeeId",
                table: "Servicos",
                column: "EmployeeId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicos_Usuarios_EmployerId",
                table: "Servicos",
                column: "EmployerId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
