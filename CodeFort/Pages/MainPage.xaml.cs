using CodeFort.DataAccess;
using CodeFort.Generator;
using CodeFort.Message;
using CodeFort.Models.Service;
using CodeFort.Pages;
using CodeFort.Storage;

namespace CodeFort
{
    public partial class MainPage : ContentPage
    {
        // Цвет темы
        bool theme = false;
        // Взаимодействие с бд
        ProgramDataService DataService;

        public MainPage(ApplicationDbContext context)
        {
            InitializeComponent();
            DataService = new ProgramDataService(context);
            InitializeUI();
        }
        private void InitializeUI()
        {
            GreetingLabel.Text = MessageProvider.GetGreeting(DateTime.Now);
            ApplyTheme(theme);
            ShowWelcomeUser();
        }
        private async void ShowWelcomeUser()
        {
            // Проверка, входит ли пользователь впервые
            var name = await SecureStorageManager.GetSavedUserName();
            if (name != null)
            {
                MessageLabel.Text = MessageProvider.WelcomeReturningUser(name);
                LoginButton.Text = "Войти";
                RemoveButton.IsVisible = true;
                NameEntry.IsVisible = false;
            }
            else
            {
                MessageLabel.Text = MessageProvider.WelcomeFirstLogin;
                LoginButton.Text = "Зарегистрироваться";
                RemoveButton.IsVisible = false;
                NameEntry.IsVisible = true;

            }
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            // Проверка пароля
            if (await SecureStorageManager.GetSavedPassword() == null)
            {
                SavePasswordAndProceed();
            }
            else
                CheckPasswordAndProceed();
            //NameEntry.Text = PasswordEntry.Text = String.Empty;
        }
        private async void SavePasswordAndProceed()
        {
            if (!string.IsNullOrEmpty(NameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text))
            {
                if (SecurePasswordGenerator.IsPasswordSecure(PasswordEntry.Text))
                {
                    if (await SecureStorageManager.SetSavedData(NameEntry.Text, PasswordEntry.Text))
                    {
                        NameEntry.Text = PasswordEntry.Text = String.Empty;
                        await Navigation.PushAsync(new ApplicationPage(DataService), true);
                    }
                    else
                    {
                        await DisplayAlert("Ошибка!", "Все пошло по *****", "ОК");
                    }
                }
                else
                {
                    await DisplayAlert("Ошибка!", MessageProvider.UnsafePassword, "ОК");
                }
            }
            else
            {
                await DisplayAlert("Ошибка!", MessageProvider.IncorrectData, "ОК");
            }
            ShowWelcomeUser();
        }
        private async void CheckPasswordAndProceed()
        {
            string? password = await SecureStorageManager.GetSavedPassword();
            if (password  == PasswordEntry.Text)
            {
                await Navigation.PushAsync(new ApplicationPage(DataService), true);
            }
            else
            {
                await DisplayAlert("Ошибка", MessageProvider.IncorrectPassword, "ОК");
            }
            NameEntry.Text = PasswordEntry.Text = String.Empty;
        }
        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            // Подтверждение перед удалением
            await DisplayAlert("Подтверждение", MessageProvider.DeleteDataConfirmation, "Да", "Отмена")
                .ContinueWith((result) =>
                {
                    if (result.Result)
                    {
                        RemoveSavedDataAndResetUI();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void RemoveSavedDataAndResetUI()
        {
            SecureStorageManager.RemoveSavedData();
            // Отчистка отображения
            ShowWelcomeUser();
            NameEntry.Text = PasswordEntry.Text = String.Empty;
            // Отчистка базы данных
            DataService.RemoveAll();
        }
        private void ThemeSwitchButton_Clicked(object sender, EventArgs e)
        {
            theme = !theme;
            ApplyTheme(theme);
        }
        private void ApplyTheme(bool darkTheme)
        {
            ThemeSwitchButton.Text = darkTheme ? "☼" : "☽";
            this.Background = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
            GreetingLabel.TextColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
            MessageLabel.TextColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
            NameEntry.TextColor = Color.FromArgb("#14212A");
            NameEntry.BackgroundColor = Color.FromArgb("#F5F8FF");
            PasswordEntry.TextColor = Color.FromArgb("#14212A");
            PasswordEntry.BackgroundColor = Color.FromArgb("#F5F8FF");
            LoginButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
            LoginButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
            RemoveButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
            RemoveButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
            ThemeSwitchButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
            ThemeSwitchButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");

            hidePasswordButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        }

        private bool isPasswordHidden = true;
        private void hidePasswordButton_Clicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = isPasswordHidden = !isPasswordHidden;
            hidePasswordButton.Text = isPasswordHidden ? "🙈" : "🙉";
        }
    }
}
