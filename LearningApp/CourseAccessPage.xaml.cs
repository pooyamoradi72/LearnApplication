using Microsoft.Maui.Controls;

namespace LearningApp
{
    public partial class CourseAccessPage : ContentPage
    {
        private readonly Course _course;
        private readonly AppDatabase _db;

        public CourseAccessPage(Course course, AppDatabase db)
        {
            InitializeComponent();
            _course = course;
            _db = db;

            LoadLessons();
        }

        private async void LoadLessons()
        {
            var lessons = await _db.GetLessonsAsync(_course.Id);
            LessonsCollection.ItemsSource = lessons;
        }

        // وقتی روی لینک کلیک شد
        private async void Link_Tapped(object sender, EventArgs e)
        {
            if (sender is Label lbl && lbl.BindingContext is Lesson lesson)
            {
                if (!string.IsNullOrEmpty(lesson.DownloadLink))
                {
                    await Browser.OpenAsync(lesson.DownloadLink, BrowserLaunchMode.SystemPreferred);
                }
            }
        }
    }
}
