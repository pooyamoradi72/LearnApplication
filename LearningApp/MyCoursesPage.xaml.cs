using System.Linq;
using Microsoft.Maui.Controls;

namespace LearningApp
{
    public partial class MyCoursesPage : ContentPage
    {
        private readonly AppDatabase _db;

        public MyCoursesPage(AppDatabase db)
        {
            InitializeComponent();
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CoursesCollection.ItemsSource = await _db.GetPurchasedCoursesAsync();
        }

        private async void Course_Selected(object sender, SelectionChangedEventArgs e)
        {
            var course = e.CurrentSelection.FirstOrDefault() as Course;
            if (course == null)
                return;

            ((CollectionView)sender).SelectedItem = null;

            await Navigation.PushAsync(new CourseAccessPage(course, _db));
        }

        private async void CartButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage(_db));
        }
    }
}
