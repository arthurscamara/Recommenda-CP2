using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recommenda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_Artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bio = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Artists", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Genres", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Salt = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReleaseDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CoverUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArtistId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_Albums_RC_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "RC_Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_ArtistGenres",
                columns: table => new
                {
                    ArtistsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GenresId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_ArtistGenres", x => new { x.ArtistsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_RC_ArtistGenres_RC_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "RC_Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC_ArtistGenres_RC_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "RC_Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPublic = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_Playlists_RC_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "RC_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Bio = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvatarUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FavoriteGenre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_UserProfiles_RC_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "RC_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_AlbumRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AlbumId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_AlbumRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_AlbumRatings_RC_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "RC_Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC_AlbumRatings_RC_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "RC_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false),
                    TrackNumber = table.Column<int>(type: "int", nullable: false),
                    AlbumId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_Tracks_RC_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "RC_Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_PlaylistTracks",
                columns: table => new
                {
                    PlaylistId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TracksId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_PlaylistTracks", x => new { x.PlaylistId, x.TracksId });
                    table.ForeignKey(
                        name: "FK_RC_PlaylistTracks_RC_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "RC_Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC_PlaylistTracks_RC_Tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "RC_Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_TrackGenres",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TracksId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_TrackGenres", x => new { x.GenresId, x.TracksId });
                    table.ForeignKey(
                        name: "FK_RC_TrackGenres_RC_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "RC_Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC_TrackGenres_RC_Tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "RC_Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RC_TrackRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TrackId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RC_TrackRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RC_TrackRatings_RC_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "RC_Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RC_TrackRatings_RC_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "RC_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RC_AlbumRatings_AlbumId",
                table: "RC_AlbumRatings",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_AlbumRatings_UserId_AlbumId",
                table: "RC_AlbumRatings",
                columns: new[] { "UserId", "AlbumId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_Albums_ArtistId",
                table: "RC_Albums",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_ArtistGenres_GenresId",
                table: "RC_ArtistGenres",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_Artists_Name",
                table: "RC_Artists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_Genres_Name",
                table: "RC_Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_Playlists_UserId",
                table: "RC_Playlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_PlaylistTracks_TracksId",
                table: "RC_PlaylistTracks",
                column: "TracksId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_TrackGenres_TracksId",
                table: "RC_TrackGenres",
                column: "TracksId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_TrackRatings_TrackId",
                table: "RC_TrackRatings",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_RC_TrackRatings_UserId_TrackId",
                table: "RC_TrackRatings",
                columns: new[] { "UserId", "TrackId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_Tracks_AlbumId_TrackNumber",
                table: "RC_Tracks",
                columns: new[] { "AlbumId", "TrackNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_UserProfiles_UserId",
                table: "RC_UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RC_Users_Email",
                table: "RC_Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RC_AlbumRatings");

            migrationBuilder.DropTable(
                name: "RC_ArtistGenres");

            migrationBuilder.DropTable(
                name: "RC_PlaylistTracks");

            migrationBuilder.DropTable(
                name: "RC_TrackGenres");

            migrationBuilder.DropTable(
                name: "RC_TrackRatings");

            migrationBuilder.DropTable(
                name: "RC_UserProfiles");

            migrationBuilder.DropTable(
                name: "RC_Playlists");

            migrationBuilder.DropTable(
                name: "RC_Genres");

            migrationBuilder.DropTable(
                name: "RC_Tracks");

            migrationBuilder.DropTable(
                name: "RC_Users");

            migrationBuilder.DropTable(
                name: "RC_Albums");

            migrationBuilder.DropTable(
                name: "RC_Artists");
        }
    }
}
