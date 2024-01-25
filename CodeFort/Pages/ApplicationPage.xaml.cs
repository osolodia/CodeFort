using CodeFort.Generator;
using CodeFort.Message;
using CodeFort.Models.Entity;
using CodeFort.Models.Service;
using Microsoft.Maui.Controls;
using System.Xml.Linq;

namespace CodeFort.Pages;

public partial class ApplicationPage : ContentPage
{
    ProgramDataService service;
    bool theme = false;
    public ApplicationPage(ProgramDataService dataService, bool theme = false)
    {
        InitializeComponent();
        BindingContext = this;
        service = dataService;
        this.theme = theme;
        ApplyTheme(theme);
        listView.ItemsSource = service.GetAll();
    }
    private void hideButton_Clicked(object sender, EventArgs e)
    {
        mainStackLayout.IsVisible = !mainStackLayout.IsVisible;
        hideButton.Text = mainStackLayout.IsVisible ? "Скрыть поля для ввода" : "Показать поля для ввода";
    }

    private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var item = (ProgramData)e.Item;
        nameEntry.Text = item.Name;
        loginEntry.Text = item.Login;
        passwordEntry.Text = item.Password;
    }


    private void saveButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(nameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text) && !string.IsNullOrEmpty(loginEntry.Text))
        {
            if (SecurePasswordGenerator.IsPasswordSecure(passwordEntry.Text))
            {
                service.Add(new ProgramData()
                {
                    Name = nameEntry.Text,
                    Login = loginEntry.Text,
                    Password = passwordEntry.Text,
                });
                listView.ItemsSource = service.GetAll();
                nameEntry.Text = loginEntry.Text = passwordEntry.Text = String.Empty;
            }
            else
            {
                DisplayAlert("Ошибка!", MessageProvider.UnsafePassword, "ОК");
            }
        }
        else
        {
            DisplayAlert("Ошибка!", MessageProvider.IncorrectData, "ОК");
        }
    }
    private async void removeButton_Clicked(object sender, EventArgs e)
    {
        var selectedItem = (ProgramData)listView.SelectedItem;
        if (selectedItem != null)
        {
            await DisplayAlert("Подтверждение", MessageProvider.DeleteItem, "Да", "Отмена")
            .ContinueWith((result) =>
            {
                if (result.Result)
                {
                    service.Remove(selectedItem);
                    listView.ItemsSource = service.GetAll();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        else
        {
            await DisplayAlert("Ошибка!", MessageProvider.IncorectData, "ОК");
        }
        nameEntry.Text = loginEntry.Text = passwordEntry.Text = String.Empty;
        listView.SelectedItem = null;
    }

    private void updateButton_Clicked(object sender, EventArgs e)
    {
        var item = (ProgramData)listView.SelectedItem;
        if (item != null)
        {
            var id = ((ProgramData)listView.SelectedItem).Id;
            if (!string.IsNullOrEmpty(nameEntry.Text) && !string.IsNullOrEmpty(passwordEntry.Text) && !string.IsNullOrEmpty(loginEntry.Text))
            {
                if (SecurePasswordGenerator.IsPasswordSecure(passwordEntry.Text))
                {
                    service.UpdateById(id, new ProgramData()
                    {
                        Name = nameEntry.Text,
                        Login = loginEntry.Text,
                        Password = passwordEntry.Text,
                    });
                    nameEntry.Text = loginEntry.Text = passwordEntry.Text = String.Empty;
                }
                else
                {
                    DisplayAlert("Ошибка!", MessageProvider.UnsafePassword, "ОК");
                }
            }
            else
            {
                DisplayAlert("Ошибка!", MessageProvider.IncorrectData, "ОК");
            }
        }
        else
        {
            DisplayAlert("Ошибка!", MessageProvider.IncorectData, "OK");
        }
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
        removeButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        removeButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        updateButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        updateButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        hidePasswordButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        hideButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        hideButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
        ThemeSwitchButton.TextColor = darkTheme ? Color.FromArgb("#14212A") : Color.FromArgb("#F5F8FF");
        ThemeSwitchButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");

        generatorPasswordButton.BackgroundColor = darkTheme ? Color.FromArgb("#03dac6") : Color.FromArgb("#018786");
    }
    private bool isPasswordHidden = true;
    private void hidePasswordButton_Clicked(object sender, EventArgs e)
    {
        passwordEntry.IsPassword = isPasswordHidden = !isPasswordHidden;
        hidePasswordButton.Text = isPasswordHidden ? "🙈" : "🙉";
    }

    private void generatorPasswordButton_Clicked(object sender, EventArgs e)
    {
        passwordEntry.Text = SecurePasswordGenerator.GenerateSecurePassword((new Random()).Next(12, 15));
    }
}