using SQLite;

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

            // ساخت یوزر پیش‌فرض
            SeedUser();


            ////using (var db = new SQLiteConnection(dbPath))
            ////{
            ////    // حذف جدول قدیمی اگر وجود دارد
            ////    db.DropTable<User>();

            ////    // ایجاد جدول جدید
            ////    db.CreateTable<User>();
            ////}
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
            return new Window(
                new NavigationPage(
                    new MainPage(Database)   // صفحه لاگین
                )
            );
        }
    }
}
