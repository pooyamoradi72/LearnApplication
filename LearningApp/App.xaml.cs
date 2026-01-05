using Microsoft.Maui.Controls;
using System.IO;

namespace LearningApp
{
    public partial class App : Application
    {
        public static AppDatabase Database { get; private set; } = null!;

        public App()
        {
            InitializeComponent();

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "learning.db");
            Database = new AppDatabase(dbPath);

            SeedUser();
        }

        private async void SeedUser()
        {
            var user = await Database.GetUserAsync("admin", "1234");

            if (user == null)
            {
                await Database.SaveUserAsync(new User
                {
                    Username = "admin",
                    Password = "1234"
                });
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // وقتی برنامه باز میشه، اول صفحه لاگین بیاد
            return new Window(
                new MainPage(Database)
            );
        }

    }
}
