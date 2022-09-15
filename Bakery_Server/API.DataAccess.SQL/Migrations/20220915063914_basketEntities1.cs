using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.DataAccess.SQL.Migrations
{
    public partial class basketEntities1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    mID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mTimeEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.mID);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    mID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    basketmID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    productmID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    qty = table.Column<int>(type: "int", nullable: false),
                    sizeSelected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mTimeEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.mID);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_basketmID",
                        column: x => x.basketmID,
                        principalTable: "Baskets",
                        principalColumn: "mID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_productmID",
                        column: x => x.productmID,
                        principalTable: "Products",
                        principalColumn: "mID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_basketmID",
                table: "BasketItems",
                column: "basketmID");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_productmID",
                table: "BasketItems",
                column: "productmID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Baskets");
        }
    }
}
