using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoKinoGo.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionItemToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedMovie");

            migrationBuilder.CreateTable(
                name: "MovieCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCollection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItem_MovieCollection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "MovieCollection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItem_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_CollectionId_MovieId",
                table: "CollectionItem",
                columns: new[] { "CollectionId", "MovieId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_CollectionId_Position",
                table: "CollectionItem",
                columns: new[] { "CollectionId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_MovieId",
                table: "CollectionItem",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItem");

            migrationBuilder.DropTable(
                name: "MovieCollection");

            migrationBuilder.CreateTable(
                name: "FeaturedMovie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedMovie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturedMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedMovie_MovieId",
                table: "FeaturedMovie",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedMovie_Position",
                table: "FeaturedMovie",
                column: "Position",
                unique: true);
        }
    }
}
