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
            await LoadUsersListAndRequestNew();
        }

        public async Task OnAddUserOptionSelected()
        {
            try
            {
                var name = view.RequestName() ?? "";
                var email = view.RequestEmail() ?? "";
                var passwword = view.RequestPassword() ?? "";

                var user = new User(name, email, passwword);

                await SaveUser(user);
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
            catch (System.Exception ex)
            {

                await ShowErrorAndShowListAndRequestNew(ex.Message);
            }
        }

        private async Task SaveUser(User user)
        {
            try
            {
                await this.saveUserUserCase.execute(user);

                await LoadUsersListAndRequestNew();
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
            await LoadUsersListAndRequestNew();
        }

        private async Task LoadUsersListAndRequestNew()
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

            await OnAddUserOptionSelected();
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
