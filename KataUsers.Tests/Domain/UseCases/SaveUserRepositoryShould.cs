using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KataUsers.Domain.Entities;
using KataUsers.Domain.Repositories;
using KataUsers.Domain.Types;
using KataUsers.Domain.UseCases;
using KataUsers.Domain.ValueObjects;
using Xunit;

namespace KataUsers.Tests.Domain.UseCases
{
    public class SaveUserRepositoryShould
    {
        List<User> users = new List<User>(){
            new User("Jorge Sánchez Fernández", "xurxodev@gmail.com", "12345678A"),
            new User("David Sánchez Fernández", "nonexisted@gmail.com", "12345678B")
        };

        [Fact]
        public async Task save_successfully_a_new_valid_user()
        {
            var useCase = GivenThereAreNoUsers();

            await useCase.execute(users[0]);
        }

        [Fact]
        public async Task throw_duplicate_resource_exception()
        {
            var useCase = GivenThereAreUsers();

            Func<Task> act = () => useCase.execute(users[0]);

            DuplicateResourceException exception = await Assert.ThrowsAsync<DuplicateResourceException>(act);
        }

        private SaveUserUserCase GivenThereAreNoUsers()
        {
            return new SaveUserUserCase(new UserRepositoryWithoutData());
        }

        private SaveUserUserCase GivenThereAreUsers()
        {
            return new SaveUserUserCase(new UserRepositoryWithData(users));
        }
    }

    class UserRepositoryWithData : UserRepository
    {
        private List<User> users = new List<User>();

        public UserRepositoryWithData(List<User> users)
        {
            this.users = users;
        }

        public Task<User?> GetByEmail(Email email)
        {
            return Task.FromResult(users.Count > 0 ? users[0] : null);
        }

        public Task<List<User>> getUsers()
        {
            return Task.FromResult(users);
        }

        public Task save(User user)
        {
            return Task.Run(() => { });
        }
    }

    class UserRepositoryWithoutData : UserRepository
    {
        public Task<User?> GetByEmail(Email email)
        {
            return Task.FromResult<User?>(null);
        }

        public Task<List<User>> getUsers()
        {
            return Task.FromResult(new List<User>());
        }

        public Task save(User user)
        {
            return Task.Run(() => { });
        }
    }
}
