using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetAPI.Migrations
{
    public partial class addnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Ticket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "price",
                table: "Ticket",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
