using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsPlan.Infrastructure.Migrations
{
    public partial class SupplierProduct_ProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "SupplierProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SupplierProduct");
        }
    }
}
