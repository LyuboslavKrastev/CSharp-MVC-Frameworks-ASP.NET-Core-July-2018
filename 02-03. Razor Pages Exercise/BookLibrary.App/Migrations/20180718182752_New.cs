using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibrary.App.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Books",
                newName: "IsBorrowed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBorrowed",
                table: "Books",
                newName: "Status");
        }
    }
}
