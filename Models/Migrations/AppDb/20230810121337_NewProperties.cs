using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class NewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_Rented",
                table: "Purchase_Details",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Clicked_By_Farmer",
                table: "Plan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Order_Details",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Clicked_By_Admin",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Clicked_By_Buyer",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Clicked_By_Team",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Item",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_Rented",
                table: "Purchase_Details");

            migrationBuilder.DropColumn(
                name: "Clicked_By_Farmer",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Order_Details");

            migrationBuilder.DropColumn(
                name: "Clicked_By_Admin",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Clicked_By_Buyer",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Clicked_By_Team",
                table: "Order");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Item",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
