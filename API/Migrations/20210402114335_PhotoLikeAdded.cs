using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class PhotoLikeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoLikes",
                columns: table => new
                {
                    LikerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PhotoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateLiked = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoLikes", x => new { x.PhotoId, x.LikerId });
                    table.ForeignKey(
                        name: "FK_PhotoLikes_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoLikes_Users_LikerId",
                        column: x => x.LikerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoLikes_LikerId",
                table: "PhotoLikes",
                column: "LikerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoLikes");
        }
    }
}
