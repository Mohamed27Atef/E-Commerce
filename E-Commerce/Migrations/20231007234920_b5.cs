using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class b5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Order_OrderId",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Order_OrderId",
                table: "Carts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Order_OrderId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Order_OrderId",
                table: "Carts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
