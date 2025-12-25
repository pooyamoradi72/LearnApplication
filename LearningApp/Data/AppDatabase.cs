
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningApp
{
    // ================= USER =================
    public class User
    {
        private SQLiteAsyncConnection _database;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    // ================= DATABASE =================
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public AppDatabase(string path)
        {
            _db = new SQLiteAsyncConnection(path);

            _db.CreateTableAsync<Course>().Wait();
            _db.CreateTableAsync<Lesson>().Wait();
            _db.CreateTableAsync<User>().Wait();

            

            // ایجاد جدول‌ها
          
        }

        // ---------- USERS ----------
        public Task<User?> GetUserAsync(string username, string password)
        {
            return _db.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _db.InsertAsync(user);
        }

        // ---------- COURSES ----------
        public Task<List<Course>> GetCoursesAsync()
            => _db.Table<Course>().ToListAsync();

        public Task<int> SaveCourseAsync(Course course)
            => _db.InsertAsync(course);

        // ---------- LESSONS ----------
        public Task<List<Lesson>> GetLessonsAsync(int courseId)
            => _db.Table<Lesson>()
                  .Where(l => l.CourseId == courseId)
                  .ToListAsync();

        public Task<int> SaveLessonAsync(Lesson lesson)
            => _db.InsertAsync(lesson);

        // گرفتن دوره بر اساس عنوان (برای آپدیت Description و Price)
        public Task<Course> GetCourseByTitleAsync(string title)
        {
            return _db.Table<Course>().Where(c => c.Title == title).FirstOrDefaultAsync();
        }

        // آپدیت اطلاعات یک دوره
        public Task<int> UpdateCourseAsync(Course course)
        {
            return _db.UpdateAsync(course);
        }

        // گرفتن جلسات بر اساس CourseId
        public Task<List<Lesson>> GetLessonsByCourseIdAsync(int courseId)
        {
            return _db.Table<Lesson>().Where(l => l.CourseId == courseId).ToListAsync();
        }



    }

}
