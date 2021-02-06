using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Polygon.Infrastructure.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "FormSchemas");

            migrationBuilder.CreateTable(
                name: "FormDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JsonData = table.Column<string>(type: "jsonb", nullable: false),
                    FormSchemaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormDatas_FormSchemas_FormSchemaId",
                        column: x => x.FormSchemaId,
                        principalTable: "FormSchemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormDatas_FormSchemaId",
                table: "FormDatas",
                column: "FormSchemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDatas");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationTimestamp",
                table: "FormSchemas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
