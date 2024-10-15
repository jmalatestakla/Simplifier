using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplifier.Entities.Migrations
{
    /// <inheritdoc />
    public partial class formFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormFields",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFields", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_FormFields_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormResponses",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormResponses", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_FormResponses_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_TemplateId",
                table: "FormFields",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormResponses_ApplicationId",
                table: "FormResponses",
                column: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormFields");

            migrationBuilder.DropTable(
                name: "FormResponses");
        }
    }
}
