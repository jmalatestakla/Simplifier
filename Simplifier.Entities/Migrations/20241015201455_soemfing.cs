using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifier.Entities.Migrations
{
    /// <inheritdoc />
    public partial class soemfing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Templates_TemplateId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_TemplateId",
                table: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applications_TemplateId",
                table: "Applications",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Templates_TemplateId",
                table: "Applications",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Uuid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
