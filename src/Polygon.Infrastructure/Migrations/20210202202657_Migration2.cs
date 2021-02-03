using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using NJsonSchema;

namespace Polygon.Infrastructure.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<JsonDocument>(
                name: "Schema",
                table: "FormSchemas",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(JsonSchema),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationTimestamp",
                table: "FormSchemas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTimestamp",
                table: "FormSchemas");

            migrationBuilder.AlterColumn<JsonSchema>(
                name: "Schema",
                table: "FormSchemas",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(JsonDocument),
                oldType: "jsonb");
        }
    }
}
