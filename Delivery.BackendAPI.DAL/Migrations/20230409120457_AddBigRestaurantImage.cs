using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delivery.BackendAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddBigRestaurantImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Restaurants",
                newName: "SmallImage");

            migrationBuilder.AddColumn<string>(
                name: "BigImage",
                table: "Restaurants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigImage",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "SmallImage",
                table: "Restaurants",
                newName: "Image");
        }
    }
}
