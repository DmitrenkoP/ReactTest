using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactTest.Migrations
{
    /// <inheritdoc />
    public partial class fix_journal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Exceptions");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Exceptions",
                newName: "StackTrace");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Exceptions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Exceptions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Exceptions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Exceptions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Exceptions");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Exceptions");

            migrationBuilder.RenameColumn(
                name: "StackTrace",
                table: "Exceptions",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Exceptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
