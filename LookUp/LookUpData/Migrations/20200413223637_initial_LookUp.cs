using Microsoft.EntityFrameworkCore.Migrations;

namespace LookUpData.Migrations
{
    public partial class initial_LookUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookUpTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LookUpTypeId = table.Column<int>(nullable: false),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LookUps_LookUpTypes_LookUpTypeId",
                        column: x => x.LookUpTypeId,
                        principalTable: "LookUpTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CascadingLookUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CascadingLookUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CascadingLookUps_LookUps_ChildId",
                        column: x => x.ChildId,
                        principalTable: "LookUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CascadingLookUps_LookUps_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LookUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CascadingLookUps_ChildId",
                table: "CascadingLookUps",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_CascadingLookUps_ParentId",
                table: "CascadingLookUps",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_LookUps_LookUpTypeId",
                table: "LookUps",
                column: "LookUpTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CascadingLookUps");

            migrationBuilder.DropTable(
                name: "LookUps");

            migrationBuilder.DropTable(
                name: "LookUpTypes");
        }
    }
}
