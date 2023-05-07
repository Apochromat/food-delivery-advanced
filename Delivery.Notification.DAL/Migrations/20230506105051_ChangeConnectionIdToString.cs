using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.Notification.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConnectionIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConnectionId",
                table: "Connections",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ConnectionId",
                table: "Connections",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
