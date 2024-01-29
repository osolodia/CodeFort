using CodeFort.Generator;
using CodeFort.Message;
using CodeFort.Models.Entity;
using CodeFort.Models.Service;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace CodeFort.Pages;

public partial class ListPage : ContentPage
{
    private ProgramDataService service;
    private bool theme = false;
    private int updatedId = 0;
    public ListPage(ProgramDataService dataService, bool theme = false)
	{
		InitializeComponent();
        BindingContext = this;
        this.theme = theme;
        service = dataService;
        ApplyTheme(theme);
        listView.ItemsSource = service.GetAll();
    }
    private bool isPasswordHidden = true;
    private void hidePasswordButton_Clicked(object sender, EventArgs e)
    {
        passwordEntry.IsPassword = isPasswordHidden = !isPasswordHidden;
        hidePasswordButton.Text = isPasswordHidden ? "🙈" : "🙉";
    }

    private void generatorPasswordButton_Clicked(object sender, EventArgs e)
    {
        passwordEntry.Text = SecurePasswordGenerator.GenerateSecurePassword();
    }

    private void hideButton_Clicked(object sender, EventArgs e)
    {
        mainStackLayout.IsVisible = !mainStackLayout.IsVisible;
        hideButton.Text = mainStackLayout.IsVisible ? "Скрыть поля для ввода" : "Показать поля для ввода";
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
        nameEntry.TextColor = Color.FromArgb("#14212A");
        nameEntry.BackgroundColor = Color.FromArgb("#F5F8FF");
        loginEntry.TextColor = Color.FromArgb("#14212A");
        loginEntry.BackgroundColor = Color.FromArgb("#F5F8FF");
        passwordEntry.TextColor = Color.FromArgb("#14212A");
        passwordEntry.BackgroundColor = Color.FromArgb("#F5F8FF");
        saveButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        saveButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        hidePasswordButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        hideButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        hideButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        ThemeSwitchButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        ThemeSwitchButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");

        generatorPasswordButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
    }
    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var item = (ProgramData)e.Item;

        if (item != null)
        {
            var action = await DisplayActionSheet("Действие", "Назад", null, "Редактировать", "Удалить", "Посмотреть пароль");
            switch (action)
            {
                case "Редактировать":
                    saveButton.Text = "Редактировать";
                    updatedId = item.Id;
                    nameEntry.Text = item.Name;
                    loginEntry.Text = item.Login;
                    passwordEntry.Text = item.Password;
                    break;
                case "Удалить":
                    service.Remove(item);
                    listView.ItemsSource = service.GetAll();
                    break;
                case "Посмотреть пароль":
                    nameEntry.Text = item.Name;
                    loginEntry.Text = item.Login;
                    passwordEntry.Text = item.Password;
                    break; 
            }
        }
        else
        {
            await DisplayAlert("Ошибка!", MessageProvider.IncorectData, "ОК");
        }
        listView.SelectedItem = null;

    }
    private void saveButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(nameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text) && !string.IsNullOrEmpty(loginEntry.Text))
        {
            if (!SecurePasswordGenerator.IsPasswordSecure(passwordEntry.Text))
            {
                DisplayAlert("Предупреждение!", MessageProvider.UnsafePassword, "ОК");
            }
            if (updatedId == 0)
            {
                service.Add(new ProgramData()
                {
                    Name = nameEntry.Text,
                    Login = loginEntry.Text,
                    Password = passwordEntry.Text
                });
            }
            else
            {
                service.UpdateById(updatedId, new ProgramData()
                {
                    Name = nameEntry.Text,
                    Login = loginEntry.Text,
                    Password = passwordEntry.Text
                });
                updatedId = 0;
                saveButton.Text = "Добавить";
            }
        }
        else
        {
            DisplayAlert("Ошибка!", MessageProvider.IncorrectData, "ОК");
        }
        nameEntry.Text = loginEntry.Text = passwordEntry.Text = String.Empty;
        listView.ItemsSource = service.GetAll();
    }
}