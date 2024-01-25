using CodeFort.Pages;

namespace CodeFort
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(ApplicationPage), typeof(ApplicationPage));
        }
    }
}
