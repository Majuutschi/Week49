using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Week49MiniProject.Migrations
{
    /// <inheritdoc />
    public partial class AddingPhonesComputers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Assets");
        }
    }
}
