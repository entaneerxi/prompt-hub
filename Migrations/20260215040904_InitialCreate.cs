using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PromptHub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prompts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SourceUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prompts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromptTags",
                columns: table => new
                {
                    PromptId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptTags", x => new { x.PromptId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PromptTags_Prompts_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromptTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(148), "การเงิน", "Finance" },
                    { 2, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(416), "เทคโนโลยีสารสนเทศ", "IT/Technology" },
                    { 3, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(418), "การตลาด", "Marketing" },
                    { 4, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(420), "การศึกษา", "Education" },
                    { 5, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(421), "การเขียนเชิงสร้างสรรค์", "Creative Writing" },
                    { 6, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(423), "ธุรกิจ", "Business" },
                    { 7, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(424), "สุขภาพ", "Health" },
                    { 8, new DateTime(2026, 2, 15, 4, 9, 3, 855, DateTimeKind.Local).AddTicks(426), "ทั่วไป", "General" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prompts_CategoryId",
                table: "Prompts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PromptTags_TagId",
                table: "PromptTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromptTags");

            migrationBuilder.DropTable(
                name: "Prompts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
