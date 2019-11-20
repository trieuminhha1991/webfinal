using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Add_Ebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PbEbooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EbookName = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false),
                    EbookDateStart = table.Column<DateTime>(nullable: true),
                    Pro = table.Column<bool>(nullable: false),
                    LinkPro = table.Column<string>(nullable: true),
                    EbookPrice = table.Column<decimal>(nullable: true),
                    EbookView = table.Column<long>(nullable: false),
                    EbookLike = table.Column<long>(nullable: false),
                    EbookDislike = table.Column<long>(nullable: false),
                    Discription = table.Column<string>(nullable: true),
                    EbookCover = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    PbClassId = table.Column<int>(nullable: true),
                    PbPlaceId = table.Column<int>(nullable: true),
                    PbRankId = table.Column<int>(nullable: false),
                    PbStatusId = table.Column<int>(nullable: false),
                    PbSubjectId = table.Column<int>(nullable: true),
                    PbSubjectEducationId = table.Column<int>(nullable: true),
                    PbTypeEbookId = table.Column<int>(nullable: false),
                    PbTypeFileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PbEbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbClasses_PbClassId",
                        column: x => x.PbClassId,
                        principalTable: "PbClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbPlaces_PbPlaceId",
                        column: x => x.PbPlaceId,
                        principalTable: "PbPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbRanks_PbRankId",
                        column: x => x.PbRankId,
                        principalTable: "PbRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbStatuses_PbStatusId",
                        column: x => x.PbStatusId,
                        principalTable: "PbStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbSubjectEducations_PbSubjectEducationId",
                        column: x => x.PbSubjectEducationId,
                        principalTable: "PbSubjectEducations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbSubjects_PbSubjectId",
                        column: x => x.PbSubjectId,
                        principalTable: "PbSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbTypeEbooks_PbTypeEbookId",
                        column: x => x.PbTypeEbookId,
                        principalTable: "PbTypeEbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PbEbooks_PbTypeFiles_PbTypeFileId",
                        column: x => x.PbTypeFileId,
                        principalTable: "PbTypeFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PbEbooks_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbClassId",
                table: "PbEbooks",
                column: "PbClassId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbPlaceId",
                table: "PbEbooks",
                column: "PbPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbRankId",
                table: "PbEbooks",
                column: "PbRankId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbStatusId",
                table: "PbEbooks",
                column: "PbStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbSubjectEducationId",
                table: "PbEbooks",
                column: "PbSubjectEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbSubjectId",
                table: "PbEbooks",
                column: "PbSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbTypeEbookId",
                table: "PbEbooks",
                column: "PbTypeEbookId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_PbTypeFileId",
                table: "PbEbooks",
                column: "PbTypeFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PbEbooks_UserId",
                table: "PbEbooks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PbEbooks");
        }
    }
}
