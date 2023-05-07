using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.AuthAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIpAddressFromDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_UserId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Devices");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Devices",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_UserId",
                table: "Devices");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Devices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
