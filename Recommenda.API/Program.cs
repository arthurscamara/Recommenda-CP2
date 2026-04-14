using Microsoft.EntityFrameworkCore;
using Recommenda.Application.Services;
using Recommenda.Infrastructure;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // -----------------------------------------------------------------------
        // Banco de dados — MySQL via Pomelo
        // -----------------------------------------------------------------------
        builder.Services.AddDbContext<RecommendaContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("RecommendaMySQL");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            // Para Oracle, comente o bloco acima e descomente abaixo:
            // options.UseOracle(builder.Configuration.GetConnectionString("RecommendaOracle"));
        });

        // -----------------------------------------------------------------------
        // Repositórios — Scoped (um por requisição HTTP)
        // -----------------------------------------------------------------------
        builder.Services.AddScoped<IArtistRepository,      ArtistRepository>();
        builder.Services.AddScoped<IAlbumRepository,       AlbumRepository>();
        builder.Services.AddScoped<ITrackRepository,       TrackRepository>();
        builder.Services.AddScoped<IGenreRepository,       GenreRepository>();
        builder.Services.AddScoped<IUserRepository,        UserRepository>();
        builder.Services.AddScoped<IAlbumRatingRepository, AlbumRatingRepository>();
        builder.Services.AddScoped<ITrackRatingRepository, TrackRatingRepository>();
        builder.Services.AddScoped<IPlaylistRepository,    PlaylistRepository>();

        // -----------------------------------------------------------------------
        // API
        // -----------------------------------------------------------------------
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        // Aplica migrations pendentes automaticamente ao iniciar
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<RecommendaContext>();
            db.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
            app.MapOpenApi();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // -----------------------------------------------------------------------
        // GET /health — demonstra leitura no banco (CP2 opção B)
        // -----------------------------------------------------------------------
        app.MapGet("/health", (RecommendaContext db) =>
        {
            var canConnect = db.Database.CanConnect();
            return Results.Ok(new
            {
                status    = canConnect ? "healthy" : "degraded",
                database  = canConnect ? "connected" : "unreachable",
                timestamp = DateTime.UtcNow
            });
        });

        app.Run();
    }
}
