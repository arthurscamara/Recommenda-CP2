using Recommenda.Application.Services;
using Recommenda.Domain.Entities;
using Recommenda.Infrastructure.Persistence;

namespace Recommenda.Infrastructure;

public sealed class UserRepository(RecommendaContext context) : IUserRepository
{
    public IReadOnlyList<User> GetAll() =>
        context.Users.OrderBy(u => u.Name).ToList();

    public User? GetById(Guid id) =>
        context.Users.FirstOrDefault(u => u.Id == id);

    public User? GetByEmail(string email)
    {
        var normalized = email.Trim().ToLower();
        return context.Users.FirstOrDefault(u => u.Email.ToLower() == normalized);
    }

    public User Create(User user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public bool ExistsByEmail(string email)
    {
        var normalized = email.Trim().ToLower();
        return context.Users.Any(u => u.Email.ToLower() == normalized);
    }
}
