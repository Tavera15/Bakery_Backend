using Microsoft.EntityFrameworkCore.Migrations;

namespace API.DataAccess.SQL.Migrations
{
    public partial class productTypeFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mProductType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mProductType",
                table: "Products");
        }
    }
}
