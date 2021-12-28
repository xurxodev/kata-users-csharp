using KataUsers.Domain.Entities;
using KataUsers.Domain.Repositories;
using KataUsers.Domain.Types;

namespace KataUsers.Domain.UseCases
{
    public class SaveUserUserCase
    {
        private UserRepository userRepository;

        public SaveUserUserCase(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task execute(User user)
        {

            var duplicatedUser = await this.userRepository.GetByEmail(user.Email);

            if (duplicatedUser != null)
            {
                throw new DuplicateResourceException($"There is already a user with email {user.Email}");
            }
            else
            {
                await this.userRepository.save(user);
            }
        }

    }
}
