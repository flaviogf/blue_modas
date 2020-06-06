using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueModas.Api.Database.Migrations
{
    public partial class v2__Create_Table_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Number = table.Column<Guid>(nullable: false),
                    Customer_Name = table.Column<string>(nullable: true),
                    Customer_Email = table.Column<string>(nullable: true),
                    Customer_Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Number);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
