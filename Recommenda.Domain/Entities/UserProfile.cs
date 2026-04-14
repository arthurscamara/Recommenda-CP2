using Recommenda.Domain.Common;

namespace Recommenda.Domain.Entities;

/// <summary>
/// Perfil público do usuário na plataforma.
/// Relacionamento 1:1 opcional com User.
/// </summary>
public class UserProfile : BaseEntity
{
    public Guid UserId { get; private set; }

    public string Bio { get; private set; } = string.Empty;

    public string AvatarUrl { get; private set; } = string.Empty;

    public string FavoriteGenre { get; private set; } = string.Empty;

    private UserProfile() { }

    public UserProfile(Guid userId, string bio = "", string avatarUrl = "", string favoriteGenre = "")
    {
        UserId = userId;
        Bio = bio;
        AvatarUrl = avatarUrl;
        FavoriteGenre = favoriteGenre;
    }

    public void Update(string bio, string avatarUrl, string favoriteGenre)
    {
        Bio = bio;
        AvatarUrl = avatarUrl;
        FavoriteGenre = favoriteGenre;
    }
}
