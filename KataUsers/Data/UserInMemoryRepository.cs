using KataUsers.Domain.Entities;
using KataUsers.Domain.Repositories;
using KataUsers.Domain.ValueObjects;

namespace KataUsers.Data
{
    public class UserInMemoryRepository : UserRepository
    {
        private List<User> users = new List<User>();

        public Task<User?> GetByEmail(Email email)
        {
            return Task.Run(() => users.Find(u => u.Email == email));
        }

        public Task<List<User>> getUsers()
        {
            return Task.Run(() => users);
        }

        public Task save(User user)
        {
            return Task.Run(() => users.Add(user));
        }
    }
}


