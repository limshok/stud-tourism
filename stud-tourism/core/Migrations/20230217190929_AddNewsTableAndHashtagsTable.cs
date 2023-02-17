using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace core.Migrations
{
    public partial class AddNewsTableAndHashtagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NewsModelId",
                table: "Images",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hashtags_News_NewsModelId",
                        column: x => x.NewsModelId,
                        principalTable: "News",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_NewsModelId",
                table: "Images",
                column: "NewsModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashtags_NewsModelId",
                table: "Hashtags",
                column: "NewsModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_News_NewsModelId",
                table: "Images",
                column: "NewsModelId",
                principalTable: "News",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_News_NewsModelId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "Hashtags");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropIndex(
                name: "IX_Images_NewsModelId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "NewsModelId",
                table: "Images");
        }
    }
}
