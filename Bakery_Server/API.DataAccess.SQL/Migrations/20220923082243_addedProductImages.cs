using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.DataAccess.SQL.Migrations
{
    public partial class addedProductImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mAvailableSizes",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    mID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productmID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    imageSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mTimeEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.mID);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_productmID",
                        column: x => x.productmID,
                        principalTable: "Products",
                        principalColumn: "mID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_productmID",
                table: "ProductImages",
                column: "productmID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropColumn(
                name: "mAvailableSizes",
                table: "Products");
        }
    }
}
