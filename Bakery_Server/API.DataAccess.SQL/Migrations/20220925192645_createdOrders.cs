using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.DataAccess.SQL.Migrations
{
    public partial class createdOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BakeryOrder",
                columns: table => new
                {
                    mID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grandTotal = table.Column<float>(type: "real", nullable: false),
                    mTimeEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakeryOrder", x => x.mID);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    mID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    parentOrdermID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productPrice = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false),
                    sizeSelected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mTimeEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.mID);
                    table.ForeignKey(
                        name: "FK_OrderItem_BakeryOrder_parentOrdermID",
                        column: x => x.parentOrdermID,
                        principalTable: "BakeryOrder",
                        principalColumn: "mID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_parentOrdermID",
                table: "OrderItem",
                column: "parentOrdermID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "BakeryOrder");
        }
    }
}
