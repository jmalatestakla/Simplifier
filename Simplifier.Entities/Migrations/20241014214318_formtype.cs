using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifier.Entities.Migrations
{
    /// <inheritdoc />
    public partial class formtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "FormFields",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "FormFields");
        }
    }
}
