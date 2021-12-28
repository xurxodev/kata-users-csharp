using KataUsers.Domain.Entities;
using KataUsers.Domain.ValueObjects;

namespace KataUsers.Domain.Repositories
{
    public interface UserRepository
    {
        Task<User?> GetByEmail(Email email);
        Task<List<User>> getUsers();
        Task save(User user);
    }
}
