using Microsoft.EntityFrameworkCore.Migrations;

namespace Polygon.Infrastructure.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormDatas_FormSchemas_FormSchemaId",
                table: "FormDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormDatas",
                table: "FormDatas");

            migrationBuilder.RenameTable(
                name: "FormDatas",
                newName: "FormData");

            migrationBuilder.RenameIndex(
                name: "IX_FormDatas_FormSchemaId",
                table: "FormData",
                newName: "IX_FormData_FormSchemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormData",
                table: "FormData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_FormSchemas_FormSchemaId",
                table: "FormData",
                column: "FormSchemaId",
                principalTable: "FormSchemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormData_FormSchemas_FormSchemaId",
                table: "FormData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormData",
                table: "FormData");

            migrationBuilder.RenameTable(
                name: "FormData",
                newName: "FormDatas");

            migrationBuilder.RenameIndex(
                name: "IX_FormData_FormSchemaId",
                table: "FormDatas",
                newName: "IX_FormDatas_FormSchemaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormDatas",
                table: "FormDatas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormDatas_FormSchemas_FormSchemaId",
                table: "FormDatas",
                column: "FormSchemaId",
                principalTable: "FormSchemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
