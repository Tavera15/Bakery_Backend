using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.DataAccess.SQL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    mID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mUnitPrice = table.Column<double>(type: "float", nullable: false),
                    mIsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    mTimeEntered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.mID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
