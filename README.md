# Recommenda — CP2

Plataforma de descoberta e avaliação musical (estilo Last.fm).
Usuários avaliam álbuns e faixas, criam playlists e acompanham artistas.

---

## Integrante

Nome: Arthur Câmara RM:562310

---

## Domínio

Sistema de descoberta e avaliação musical. Os usuários se cadastram, avaliam álbuns e faixas com notas de 1 a 5, montam playlists personalizadas e exploram artistas por gênero musical.

---

## Entidades modeladas

| Entidade | Descrição |
|----------|-----------|
| `User` | Usuário da plataforma |
| `UserProfile` | Perfil público do usuário (bio, avatar) — 1:1 opcional |
| `Artist` | Artista ou banda musical |
| `Album` | Álbum de estúdio ou EP de um artista |
| `Track` | Faixa musical dentro de um álbum |
| `Genre` | Gênero musical (Rock, Jazz, etc.) |
| `AlbumRating` | Avaliação (1–5) de um usuário sobre um álbum |
| `TrackRating` | Avaliação (1–5) de um usuário sobre uma faixa |
| `Playlist` | Coleção de faixas criada por um usuário |

---

## Relacionamentos

| Relação | Cardinalidade | Detalhe |
|---------|--------------|---------|
| Artist → Album | 1 : N | Um artista tem vários álbuns |
| Album → Track | 1 : N | Um álbum tem várias faixas |
| User → AlbumRating | 1 : N | Um usuário avalia vários álbuns |
| User → TrackRating | 1 : N | Um usuário avalia várias faixas |
| User → Playlist | 1 : N | Um usuário cria várias playlists |
| User → UserProfile | 1 : 1 | Perfil público opcional |
| Artist ↔ Genre | N : N | Tabela `RC_ArtistGenres` |
| Track ↔ Genre | N : N | Tabela `RC_TrackGenres` |
| Playlist ↔ Track | N : N | Tabela `RC_PlaylistTracks` |

---

## SGBD utilizado

**MySQL 8** via driver **Pomelo.EntityFrameworkCore.MySql 9.0.0**.

---

## Como executar

### Pré-requisitos

- .NET 10 SDK
- MySQL 8 em execução (local ou Docker)
- Ferramenta EF Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

### 1. Configurar a connection string

Use User Secrets para não expor credenciais no repositório:

```bash
cd Recommenda.API
dotnet user-secrets set "ConnectionStrings:RecommendaMySQL" \
  "Server=127.0.0.1;Port=3306;Database=recommenda_music;User=root;Password=SuaSenha;"
```

O `appsettings.json` já contém um exemplo com placeholder — **nunca commite senhas reais**.

### 2. Gerar e aplicar a migration inicial

```bash
dotnet ef migrations add Initial \
  --project Recommenda.Infrastructure \
  --startup-project Recommenda.API

dotnet ef database update \
  --project Recommenda.Infrastructure \
  --startup-project Recommenda.API
```

A aplicação também aplica migrations automaticamente ao iniciar (`db.Database.Migrate()` no `Program.cs`).

### 3. Executar a API

```bash
cd Recommenda.API
dotnet run
```

### 4. Verificar saúde do banco

```
GET http://localhost:5283/health
```

Resposta esperada:
```json
{
  "status": "healthy",
  "database": "connected",
  "timestamp": "2026-04-06T..."
}
```

---

## Endpoints disponíveis

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/health` | Verifica conexão com o banco |
| GET | `/api/artist` | Lista todos os artistas |
| GET | `/api/artist/{id}` | Busca artista por ID |
| POST | `/api/artist` | Cadastra um artista |
| DELETE | `/api/artist/{id}` | Remove um artista |
| GET | `/api/album` | Lista todos os álbuns |
| GET | `/api/album/{id}` | Busca álbum por ID |
| GET | `/api/album/artist/{artistId}` | Álbuns de um artista |
| POST | `/api/album` | Cadastra um álbum |
| DELETE | `/api/album/{id}` | Remove um álbum |
| GET | `/api/albumrating/album/{albumId}` | Avaliações de um álbum |
| GET | `/api/albumrating/user/{userId}` | Avaliações de um usuário |
| POST | `/api/albumrating` | Registra avaliação de álbum |

---

## Arquitetura (Clean Architecture)

```
Recommenda.sln
├── Recommenda.Domain          — Entidades, enums (sem dependências externas)
├── Recommenda.Application     — Interfaces de repositório, DTOs
├── Recommenda.Infrastructure  — DbContext, Fluent API, repositórios, Migrations
└── Recommenda.API             — Controllers, Program.cs, DI
```

### Repositórios registrados no DI

| Interface (Application) | Implementação (Infrastructure) |
|------------------------|-------------------------------|
| `IArtistRepository` | `ArtistRepository` |
| `IAlbumRepository` | `AlbumRepository` |
| `ITrackRepository` | `TrackRepository` |
| `IGenreRepository` | `GenreRepository` |
| `IUserRepository` | `UserRepository` |
| `IAlbumRatingRepository` | `AlbumRatingRepository` |
| `ITrackRatingRepository` | `TrackRatingRepository` |
| `IPlaylistRepository` | `PlaylistRepository` |

---

## Esquema físico

Veja `/docs/schema.md` para o diagrama de tabelas gerado pela migration.
