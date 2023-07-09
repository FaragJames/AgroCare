using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class DeleteAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Purchase_Details");
            migrationBuilder.Sql("DELETE FROM Purchase");
            migrationBuilder.Sql("DELETE FROM Store");
            migrationBuilder.Sql("DELETE FROM Store_Type");
            migrationBuilder.Sql("DELETE FROM Step_Details");
            migrationBuilder.Sql("DELETE FROM Agricultural_Item");
            migrationBuilder.Sql("DELETE FROM Step");
            migrationBuilder.Sql("DELETE FROM Action");
            migrationBuilder.Sql("DELETE FROM [Plan]");
            migrationBuilder.Sql("DELETE FROM Land");
            migrationBuilder.Sql("DELETE FROM Farmer");
            migrationBuilder.Sql("DELETE FROM Land_Type");
            migrationBuilder.Sql("DELETE FROM Soil_Type");
            migrationBuilder.Sql("DELETE FROM Order_Details");
            migrationBuilder.Sql("DELETE FROM Item");
            migrationBuilder.Sql("DELETE FROM [Order]");
            migrationBuilder.Sql("DELETE FROM Buyer");
            migrationBuilder.Sql("DELETE FROM Engineer");
            migrationBuilder.Sql("DELETE FROM Engineer_Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
