using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Add_DownloadEbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PbDownloadEbooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<long>(nullable: false),
                    Month = table.Column<DateTime>(nullable: false),
                    PbEbookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PbDownloadEbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PbDownloadEbooks_PbEbooks_PbEbookId",
                        column: x => x.PbEbookId,
                        principalTable: "PbEbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PbDownloadEbooks_PbEbookId",
                table: "PbDownloadEbooks",
                column: "PbEbookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PbDownloadEbooks");
        }
    }
}
