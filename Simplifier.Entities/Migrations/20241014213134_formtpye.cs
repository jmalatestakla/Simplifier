using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifier.Entities.Migrations
{
    /// <inheritdoc />
    public partial class formtpye : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Response",
                table: "FormFields",
                newName: "FormType");

            migrationBuilder.AddColumn<string>(
                name: "ExpectedResponse",
                table: "FormFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedResponse",
                table: "FormFields");

            migrationBuilder.RenameColumn(
                name: "FormType",
                table: "FormFields",
                newName: "Response");
        }
    }
}
