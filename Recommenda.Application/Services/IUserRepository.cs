using Recommenda.Domain.Entities;

namespace Recommenda.Application.Services;

/// <summary>Define operações de repositório para usuários.</summary>
public interface IUserRepository
{
    IReadOnlyList<User> GetAll();
    User? GetById(Guid id);
    User? GetByEmail(string email);
    User Create(User user);
    bool ExistsByEmail(string email);
}
