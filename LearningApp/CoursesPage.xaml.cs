using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningApp
{
    public partial class CoursesPage : ContentPage
    {
        private AppDatabase _db;

        public CoursesPage(AppDatabase db)
        {
            InitializeComponent();
            _db = db;
            LoadCourses();
        }

        private async void LoadCourses()
        {
            var courses = await _db.GetCoursesAsync();

            // دوره‌ها با توضیحات خودت
            var sampleCourses = new List<Course>
            {
                new Course { Title="C#", Description="زبان برنامه‌نویسی قدرتمند و شیءگرا از مایکروسافت برای ساخت برنامه‌های دسکتاپ، وب و موبایل. سینتکس ساده و امکانات پیشرفته‌ای مثل LINQ و async دارد", Image="dotnet_course.png", Price="200,000 تومان" },
                new Course { Title="MAUI", Description="فریم‌ورک مایکروسافت برای ساخت برنامه‌های کراس‌پلتفرم (اندروید، iOS، ویندوز، مک) با یک کد بیس. از C# و XAML برای رابط کاربری استفاده می‌کند.", Image="maui_course.png", Price="250,000 تومان" },
                new Course { Title="Python", Description="زبان برنامه‌نویسی ساده و قدرتمند برای توسعه وب، داده‌کاوی و اتوماسیون. یادگیری آن آسان و کاربردهای گسترده‌ای دارد.", Image="python_course.png", Price="180,000 تومان" },
                new Course { Title="JavaScript", Description="زبان برنامه‌نویسی برای وب که صفحات تعاملی و پویا ایجاد می‌کند. معمولاً همراه HTML و CSS استفاده می‌شود.", Image="js_course.png", Price="180,000 تومان" },
                new Course { Title="SQL", Description="زبان استاندارد مدیریت و پرس‌وجوی پایگاه داده‌ها. برای ذخیره، بازیابی و مدیریت اطلاعات در دیتابیس‌ها استفاده می‌شود.", Image="sql_course.png", Price="150,000 تومان" },
                new Course { Title="Word", Description="نرم‌افزاری برای تایپ و ویرایش متون، ایجاد نامه، گزارش و کتاب. امکاناتی مثل قالب‌بندی متن، جداول و تصویرگذاری دارد.", Image="word_course.png", Price="120,000 تومان" },
                new Course { Title="Excel", Description="نرم‌افزاری برای کار با جدول‌ها، محاسبات ریاضی و نمودارها.\nقابلیت تحلیل داده و استفاده از فرمول‌ها و توابع را دارد.", Image="excel_course.png", Price="130,000 تومان" },
                new Course { Title="PowerPoint", Description="نرم‌افزاری برای ساخت اسلاید و ارائه مطالب با متن، تصویر، نمودار و انیمیشن.\nبرای پرزنتیشن‌های آموزشی و کاری استفاده می‌شود.", Image="ppt_course.png", Price="120,000 تومان" },
                new Course { Title="Command Line", Description="محیط ساده متنی ویندوز برای اجرای دستورات سیستم عامل.\nبرای کار با فایل‌ها، فولدرها و برنامه‌ها کاربرد دارد.", Image="cmd_course.png", Price="100,000 تومان" },
                new Course { Title="PowerShell", Description="محیط خط فرمان پیشرفته ویندوز برای اجرای دستورات و مدیریت سیستم.\nقابلیت اتوماسیون وظایف و اسکریپت‌نویسی دارد.", Image="powershell_course.png", Price="110,000 تومان" }
            };

            foreach (var c in sampleCourses)
            {
                // بررسی اینکه دوره قبلاً وجود دارد یا نه
                var existing = await _db.GetCourseByTitleAsync(c.Title);
                if (existing == null)
                {
                    // ذخیره دوره جدید
                    await _db.SaveCourseAsync(c);
                }
                else
                {
                    // آپدیت توضیحات، تصویر و قیمت دوره موجود
                    existing.Description = c.Description;
                    existing.Image = c.Image;
                    existing.Price = c.Price;
                    await _db.UpdateCourseAsync(existing);
                }

                // جلسات دوره‌ها
                var lessons = await _db.GetLessonsAsync(c.Id);

                if (lessons.Count == 0)
                {
                    if (c.Title == "C#")
                    {
                        await _db.SaveLessonAsync(new Lesson { CourseId = c.Id, Title = "جلسه 1", DownloadLink = "https://example.com/csharp_lesson1.mp4" });
                        await _db.SaveLessonAsync(new Lesson { CourseId = c.Id, Title = "جلسه 2", DownloadLink = "https://example.com/csharp_lesson2.mp4" });
                        await _db.SaveLessonAsync(new Lesson { CourseId = c.Id, Title = "جلسه 3", DownloadLink = "https://example.com/csharp_lesson3.mp4" });
                        await _db.SaveLessonAsync(new Lesson { CourseId = c.Id, Title = "جلسه 4", DownloadLink = "https://example.com/csharp_lesson4.mp4" });
                    }
                    else
                    {
                        await _db.SaveLessonAsync(new Lesson { CourseId = c.Id, Title = "جلسه 1", DownloadLink = "https://example.com/lesson1.mp4" });
                        await _db.SaveLessonAsync(new Lesson { CourseId = c.Id, Title = "جلسه 2", DownloadLink = "https://example.com/lesson2.mp4" });
                    }
                }
            }

            // بارگذاری نهایی در CollectionView
            CoursesCollection.ItemsSource = await _db.GetCoursesAsync();
        }

        private async void CoursesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedCourse = e.CurrentSelection[0] as Course;
                if (selectedCourse != null)
                {
                    await Navigation.PushAsync(new CourseDetailsPage(selectedCourse, _db));
                }
            }
        }
//        private void ShowDatabasePath()
//        {
//            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "learning.db");

//            DisplayAlert("مسیر دیتابیس", dbPath, "باشه");

//#if WINDOWS
//                        var desktopPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "learning_copy.db");
//                        if (File.Exists(dbPath))
//                        {
//                            File.Copy(dbPath, desktopPath, overwrite: true);
//                            DisplayAlert("کپی شد", $"فایل دیتابیس روی دسکتاپ کپی شد:\n{desktopPath}", "باشه");
//                        }
//#endif
//        }

//        private void ShowDbButton_Clicked(object sender, EventArgs e)
//        {
//            ShowDatabasePath();
//        }
    }
}
