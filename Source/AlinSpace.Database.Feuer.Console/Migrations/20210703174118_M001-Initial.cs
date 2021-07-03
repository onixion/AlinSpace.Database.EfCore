using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AlinSpace.Database.Feuer.Console.Migrations
{
    public partial class M001Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Firstname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Lastname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageGroup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IsListed = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageGroup_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IsListed = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    BodyHtml = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    PageGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_PageGroup_PageGroupId",
                        column: x => x.PageGroupId,
                        principalTable: "PageGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Page_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndexPageId = table.Column<long>(type: "bigint", nullable: true),
                    ContactPageId = table.Column<long>(type: "bigint", nullable: true),
                    AboutPageId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configuration_Page_AboutPageId",
                        column: x => x.AboutPageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configuration_Page_ContactPageId",
                        column: x => x.ContactPageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configuration_Page_IndexPageId",
                        column: x => x.IndexPageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    IsListed = table.Column<bool>(type: "boolean", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RepositoryUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PageId = table.Column<long>(type: "bigint", nullable: true),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PageGroupId = table.Column<long>(type: "bigint", nullable: true),
                    PageId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Tag_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_PageGroup_PageGroupId",
                        column: x => x.PageGroupId,
                        principalTable: "PageGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_AboutPageId",
                table: "Configuration",
                column: "AboutPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_ContactPageId",
                table: "Configuration",
                column: "ContactPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_IndexPageId",
                table: "Configuration",
                column: "IndexPageId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_OwnerId",
                table: "Page",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_PageGroupId",
                table: "Page",
                column: "PageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PageGroup_OwnerId",
                table: "PageGroup",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_OwnerId",
                table: "Project",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PageId",
                table: "Project",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PageGroupId",
                table: "Tag",
                column: "PageGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PageId",
                table: "Tag",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "PageGroup");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
