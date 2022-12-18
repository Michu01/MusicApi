using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlaylistDTOIsPrivate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Genres_GenreDTOId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Genres_GenreDTOId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Genres_GenreDTOId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenreDTOId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_GenreDTOId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_GenreDTOId",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_GenreDTOId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Albums_GenreDTOId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "GenreDTOId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "GenreDTOId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "GenreDTOId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "GenreDTOId",
                table: "Albums");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Playlists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Playlists");

            migrationBuilder.AddColumn<Guid>(
                name: "GenreDTOId",
                table: "Songs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreDTOId",
                table: "Playlists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreDTOId",
                table: "Artists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenreDTOId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreDTOId",
                table: "Songs",
                column: "GenreDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_GenreDTOId",
                table: "Playlists",
                column: "GenreDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_GenreDTOId",
                table: "Artists",
                column: "GenreDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_GenreDTOId",
                table: "Albums",
                column: "GenreDTOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Genres_GenreDTOId",
                table: "Albums",
                column: "GenreDTOId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Genres_GenreDTOId",
                table: "Artists",
                column: "GenreDTOId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Genres_GenreDTOId",
                table: "Playlists",
                column: "GenreDTOId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenreDTOId",
                table: "Songs",
                column: "GenreDTOId",
                principalTable: "Genres",
                principalColumn: "Id");
        }
    }
}
