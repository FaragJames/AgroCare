using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class ImageProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_Path",
                table: "Store",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Store",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image_Path",
                table: "Item",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Path",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "Image_Path",
                table: "Item");
        }
    }
}
