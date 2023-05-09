using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.BackendAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderDishArchivedDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArchivedDishDescription",
                table: "OrderDish",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArchivedDishImageUrl",
                table: "OrderDish",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedDishDescription",
                table: "OrderDish");

            migrationBuilder.DropColumn(
                name: "ArchivedDishImageUrl",
                table: "OrderDish");
        }
    }
}
