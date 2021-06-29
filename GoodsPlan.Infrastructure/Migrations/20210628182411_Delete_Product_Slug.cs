using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsPlan.Infrastructure.Migrations
{
    public partial class Delete_Product_Slug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
