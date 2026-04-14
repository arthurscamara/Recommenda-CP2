# Esquema Físico — Recommenda Music

Tabelas geradas pela migration inicial do EF Core (MySQL 8).

## Tabelas principais

```
RC_Users
  Id           CHAR(36)      PK
  Name         VARCHAR(100)  NOT NULL
  Email        VARCHAR(255)  NOT NULL  UNIQUE
  Password     VARCHAR(500)  NOT NULL
  Salt         VARCHAR(100)  NOT NULL
  BirthDate    DATETIME      NOT NULL
  Active       TINYINT(1)    NOT NULL
  CreatedAt    DATETIME      NOT NULL

RC_UserProfiles
  Id              CHAR(36)      PK
  UserId          CHAR(36)      NOT NULL  FK → RC_Users(Id)  UNIQUE
  Bio             VARCHAR(1000)
  AvatarUrl       VARCHAR(500)
  FavoriteGenre   VARCHAR(100)
  Active          TINYINT(1)
  CreatedAt       DATETIME

RC_Artists
  Id         CHAR(36)      PK
  Name       VARCHAR(200)  NOT NULL  UNIQUE
  Bio        VARCHAR(2000)
  Country    VARCHAR(100)  NOT NULL
  Active     TINYINT(1)
  CreatedAt  DATETIME

RC_Genres
  Id           CHAR(36)     PK
  Name         VARCHAR(100) NOT NULL  UNIQUE
  Description  VARCHAR(500)
  Active       TINYINT(1)
  CreatedAt    DATETIME

RC_Albums
  Id           CHAR(36)     PK
  Title        VARCHAR(200) NOT NULL
  ReleaseDate  DATETIME     NOT NULL
  CoverUrl     VARCHAR(500)
  ArtistId     CHAR(36)     NOT NULL  FK → RC_Artists(Id)
  Active       TINYINT(1)
  CreatedAt    DATETIME

RC_Tracks
  Id              CHAR(36)     PK
  Title           VARCHAR(200) NOT NULL
  DurationSeconds INT          NOT NULL
  TrackNumber     INT          NOT NULL
  AlbumId         CHAR(36)     NOT NULL  FK → RC_Albums(Id)
  Active          TINYINT(1)
  CreatedAt       DATETIME
  UNIQUE (AlbumId, TrackNumber)

RC_AlbumRatings
  Id        CHAR(36)       PK
  UserId    CHAR(36)       NOT NULL  FK → RC_Users(Id)
  AlbumId   CHAR(36)       NOT NULL  FK → RC_Albums(Id)
  Score     INT            NOT NULL
  Comment   VARCHAR(1000)
  Active    TINYINT(1)
  CreatedAt DATETIME
  UNIQUE (UserId, AlbumId)

RC_TrackRatings
  Id        CHAR(36)       PK
  UserId    CHAR(36)       NOT NULL  FK → RC_Users(Id)
  TrackId   CHAR(36)       NOT NULL  FK → RC_Tracks(Id)
  Score     INT            NOT NULL
  Comment   VARCHAR(1000)
  Active    TINYINT(1)
  CreatedAt DATETIME
  UNIQUE (UserId, TrackId)

RC_Playlists
  Id          CHAR(36)     PK
  Name        VARCHAR(150) NOT NULL
  Description VARCHAR(500)
  IsPublic    TINYINT(1)   DEFAULT 1
  UserId      CHAR(36)     NOT NULL  FK → RC_Users(Id)
  Active      TINYINT(1)
  CreatedAt   DATETIME
```

## Tabelas de junção (N:N)

```
RC_ArtistGenres
  ArtistsId  CHAR(36)  PK+FK → RC_Artists(Id)
  GenresId   CHAR(36)  PK+FK → RC_Genres(Id)

RC_TrackGenres
  TracksId   CHAR(36)  PK+FK → RC_Tracks(Id)
  GenresId   CHAR(36)  PK+FK → RC_Genres(Id)

RC_PlaylistTracks
  PlaylistsId  CHAR(36)  PK+FK → RC_Playlists(Id)
  TracksId     CHAR(36)  PK+FK → RC_Tracks(Id)
```
