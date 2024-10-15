using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifier.Entities.Migrations
{
    /// <inheritdoc />
    public partial class fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormFields",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "FormResponses",
                table: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormFields",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FormResponses",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
