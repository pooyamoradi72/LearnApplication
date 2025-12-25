using Microsoft.Maui.Controls;

namespace LearningApp
{
    public partial class MainPage : ContentPage
    {
        private AppDatabase _db;

        public MainPage(AppDatabase db)
        {
            InitializeComponent();
            _db = db;
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var user = await _db.GetUserAsync(
                UsernameEntry.Text,
                PasswordEntry.Text
            );

            if (user != null)
            {
                await DisplayAlert("موفق", "ورود انجام شد", "باشه");
                await Navigation.PushAsync(new CoursesPage(_db));
            }
            else
            {
                await DisplayAlert("خطا", "نام کاربری یا رمز عبور اشتباه است", "باشه");
            }
        }


        private async void ForgotPassword_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("بازیابی رمز", "امکان بازیابی رمز هنوز فعال نیست", "باشه");
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

    }
}
