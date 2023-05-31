using KataUsers.Domain.Entities;
using KataUsers.Domain.Types;
using KataUsers.Domain.UseCases;

namespace KataUsers.Presentation
{
    public class UsersPresenter
    {
        private IUsersView view;
        private GetUsersUserCase getUsersUserCase;
        private SaveUserUserCase saveUserUserCase;

        public UsersPresenter(IUsersView view, GetUsersUserCase getUsersUserCase, SaveUserUserCase saveUserUserCase)
        {
            this.view = view;
            this.getUsersUserCase = getUsersUserCase;
            this.saveUserUserCase = saveUserUserCase;
        }

        public async Task OnInitialize()
        {
            view.ShowWelcomeMessage();
            await LoadUsersAndRequestNew();
        }


        private async Task LoadUsersAndRequestNew()
        {
            try
            {
                var users = await this.getUsersUserCase.execute();
                if (users.Count == 0)
                {
                    this.view.ShowEmptyCase();
                }
                else
                {
                    this.view.ShowUsers(users);
                }
            }
            catch (System.Exception ex)
            {

                this.view.ShowError(ex.Message);
            }

            await RequestNewUser();
        }

        public async Task RequestNewUser()
        {
            var name = view.RequestName() ?? "";
            var email = view.RequestEmail() ?? "";
            var passwword = view.RequestPassword() ?? "";


            await SaveUser(name, email, passwword);
        }

        private async Task SaveUser(string name, string email, string passwword)
        {
            try
            {
                var user = new User(name, email, passwword);

                await this.saveUserUserCase.execute(user);

                await LoadUsersAndRequestNew();
            }
            catch (ValidateException ex)
            {
                var messages = ex.Errors
                            .SelectMany(errorsByField =>
                                errorsByField.errors.Select(err =>
                                    Errors.ValidationErrorMessages[err](errorsByField.field)
                                )
                            );
                await ShowErrorAndShowListAndRequestNew(String.Join("\n", messages));
            }
            catch (DuplicateResourceException ex)
            {
                await ShowErrorAndShowListAndRequestNew(ex.Message);
            }
            catch (System.Exception ex)
            {
                await ShowErrorAndShowListAndRequestNew(ex.Message);
            }
        }

        private async Task ShowErrorAndShowListAndRequestNew(string message)
        {
            view.ShowError(message);
            await LoadUsersAndRequestNew();
        }
    }

    public interface IUsersView
    {
        void ShowError(string message);

        void ShowEmptyCase();

        void ShowWelcomeMessage();

        void ShowUsers(List<User> users);

        string? RequestName();

        string? RequestEmail();

        string? RequestPassword();
    }
}
