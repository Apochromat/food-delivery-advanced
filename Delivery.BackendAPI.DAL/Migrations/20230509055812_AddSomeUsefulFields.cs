using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.BackendAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeUsefulFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDish_Dishes_DishId",
                table: "OrderDish");

            migrationBuilder.AlterColumn<Guid>(
                name: "DishId",
                table: "OrderDish",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "DishesInCart",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DishesInCart_RestaurantId",
                table: "DishesInCart",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesInCart_Restaurants_RestaurantId",
                table: "DishesInCart",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDish_Dishes_DishId",
                table: "OrderDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesInCart_Restaurants_RestaurantId",
                table: "DishesInCart");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDish_Dishes_DishId",
                table: "OrderDish");

            migrationBuilder.DropIndex(
                name: "IX_DishesInCart_RestaurantId",
                table: "DishesInCart");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "DishesInCart");

            migrationBuilder.AlterColumn<Guid>(
                name: "DishId",
                table: "OrderDish",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDish_Dishes_DishId",
                table: "OrderDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id");
        }
    }
}
