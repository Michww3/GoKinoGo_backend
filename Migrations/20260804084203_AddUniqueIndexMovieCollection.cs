using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoKinoGo.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexMovieCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItem_MovieCollection_CollectionId",
                table: "CollectionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItem_Movies_MovieId",
                table: "CollectionItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCollection",
                table: "MovieCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionItem",
                table: "CollectionItem");

            migrationBuilder.RenameTable(
                name: "MovieCollection",
                newName: "MovieCollections");

            migrationBuilder.RenameTable(
                name: "CollectionItem",
                newName: "CollectionItems");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionItem_MovieId",
                table: "CollectionItems",
                newName: "IX_CollectionItems_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionItem_CollectionId_Position",
                table: "CollectionItems",
                newName: "IX_CollectionItems_CollectionId_Position");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionItem_CollectionId_MovieId",
                table: "CollectionItems",
                newName: "IX_CollectionItems_CollectionId_MovieId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MovieCollections",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCollections",
                table: "MovieCollections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionItems",
                table: "CollectionItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieCollections_Name",
                table: "MovieCollections",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_MovieCollections_CollectionId",
                table: "CollectionItems",
                column: "CollectionId",
                principalTable: "MovieCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItems_Movies_MovieId",
                table: "CollectionItems",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_MovieCollections_CollectionId",
                table: "CollectionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionItems_Movies_MovieId",
                table: "CollectionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieCollections",
                table: "MovieCollections");

            migrationBuilder.DropIndex(
                name: "IX_MovieCollections_Name",
                table: "MovieCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionItems",
                table: "CollectionItems");

            migrationBuilder.RenameTable(
                name: "MovieCollections",
                newName: "MovieCollection");

            migrationBuilder.RenameTable(
                name: "CollectionItems",
                newName: "CollectionItem");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionItems_MovieId",
                table: "CollectionItem",
                newName: "IX_CollectionItem_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionItems_CollectionId_Position",
                table: "CollectionItem",
                newName: "IX_CollectionItem_CollectionId_Position");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionItems_CollectionId_MovieId",
                table: "CollectionItem",
                newName: "IX_CollectionItem_CollectionId_MovieId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MovieCollection",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieCollection",
                table: "MovieCollection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionItem",
                table: "CollectionItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItem_MovieCollection_CollectionId",
                table: "CollectionItem",
                column: "CollectionId",
                principalTable: "MovieCollection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionItem_Movies_MovieId",
                table: "CollectionItem",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
