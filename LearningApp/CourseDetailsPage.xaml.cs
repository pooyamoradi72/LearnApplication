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
            var lessons = await _db.GetLessonsAsync(_course.Id);
            LessonsCollection.ItemsSource = lessons;
        }

        // این امضای متد حتماً باید دقیقاً همین باشه
        private async void Lessons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var lesson = e.CurrentSelection[0] as Lesson;
                if (lesson != null && !string.IsNullOrEmpty(lesson.DownloadLink))
                {
                    await Browser.OpenAsync(lesson.DownloadLink, BrowserLaunchMode.SystemPreferred);
                }
            }
        }
        private async void BuyButton_Clicked(object sender, EventArgs e)
        {
            // پیام کوتاه
            await DisplayAlert("دسترسی به دوره", "دسترسی به دوره باز شد", "باشه");

            // رفتن به صفحه CourseAccessPage
            await Navigation.PushAsync(new CourseAccessPage(_course, _db));
        }
    }
}
