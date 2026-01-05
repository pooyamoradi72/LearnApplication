using Microsoft.Maui.Controls;

namespace LearningApp
{
    public partial class CourseDetailsPage : ContentPage
    {
        private readonly Course _course;
        private readonly AppDatabase _db;

        public CourseDetailsPage(Course course, AppDatabase db)
        {
            InitializeComponent();
            _course = course;
            _db = db;
            BindingContext = _course;

            LoadLessons();
        }

        private async void LoadLessons()
        {
            LessonsCollection.ItemsSource = await _db.GetLessonsAsync(_course.Id);
        }

        private void Lessons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
            // اضافه کردن دوره به سبد خرید
            await _db.AddToCartAsync(new CartItem { CourseId = _course.Id });

            // پیام کوتاه به کاربر
            await DisplayAlert(
                "سبد خرید",
                "دوره به سبد خرید اضافه شد",
                "باشه"
            );

            // رفتن خودکار به CartPage
            await Navigation.PushAsync(new CartPage(_db));
        }

    }
}
