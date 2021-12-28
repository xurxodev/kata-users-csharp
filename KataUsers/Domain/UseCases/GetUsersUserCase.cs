

using KataUsers.Domain.Entities;
using KataUsers.Domain.Repositories;

namespace KataUsers.Domain.UseCases
{
    public class GetUsersUserCase
    {
        private UserRepository userRepository;

        public GetUsersUserCase(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Task<List<User>> execute()
        {
            return this.userRepository.getUsers();
        }
    }
}
