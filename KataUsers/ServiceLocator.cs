
using KataUsers.Data;
using KataUsers.Domain.UseCases;
using KataUsers.Presentation;

public static class ServiceLocator
{
    public static UsersPresenter GetUsersPresenter(IUsersView view)
    {
        var userRepository = new UserInMemoryRepository();
        var getUsers = new GetUsersUserCase(userRepository);
        var saveUser = new SaveUserUserCase(userRepository);

        return new UsersPresenter(view, getUsers, saveUser);
    }
}


