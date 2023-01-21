using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeBean.Migrations
{
    public partial class CategoryImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CategoryImg",
                table: "Categories",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImg",
                table: "Categories");
        }
    }
}
