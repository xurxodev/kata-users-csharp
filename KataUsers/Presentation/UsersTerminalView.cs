
using KataUsers.Domain.Entities;

namespace KataUsers.Presentation
{
    public class UsersTerminalView : IUsersView
    {
        private UsersPresenter presenter;

        public UsersTerminalView()
        {
            presenter = ServiceLocator.GetUsersPresenter(this);
        }

        public async Task show()
        {
            await presenter.OnInitialize();
        }

        public string? RequestName()
        {
            Console.WriteLine("Creating new user");
            Console.Write("Name?");
            return Console.ReadLine();
        }

        public string? RequestEmail()
        {
            Console.Write("Email?");
            return Console.ReadLine();
        }


        public string? RequestPassword()
        {
            Console.Write("Password?");
            return Console.ReadLine();
        }

        public void ShowEmptyCase()
        {
            Console.WriteLine("Users is empty! :(");
        }

        public void ShowError(string message)
        {
            Console.WriteLine("Ups, something went wrong :(");
            Console.WriteLine(message);
            Console.WriteLine("Try again!");
        }

        public void ShowGoodbyeMessage()
        {
            Console.WriteLine("See you soon!");
        }

        public void ShowUsers(List<User> users)
        {
            Console.WriteLine("Users!:");

            users.ForEach(user =>
            {
                Console.WriteLine($"{user.Name}- {user.Email}");
            });
        }

        public void ShowWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Welcome to your users kata csharp :)");
        }
    }
}


