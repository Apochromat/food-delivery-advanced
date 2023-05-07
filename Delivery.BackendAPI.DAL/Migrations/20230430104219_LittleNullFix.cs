using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.BackendAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class LittleNullFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "Managers",
                table: "Restaurants",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "Cooks",
                table: "Restaurants",
                type: "uuid[]",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Restaurants",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "Managers",
                table: "Restaurants",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "Cooks",
                table: "Restaurants",
                type: "uuid[]",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "uuid[]");
        }
    }
}
