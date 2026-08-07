using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoKinoGo.Migrations
{
    /// <inheritdoc />
    public partial class AddFeaturedMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedMovie");
        }
    }
}
