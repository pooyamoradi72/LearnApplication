using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace LearningApp
{
    // ================= USER =================
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }

    // ================= PURCHASED COURSE =================
    public class PurchasedCourse
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; }
    }

    // ================= DATABASE =================
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _db;

        public AppDatabase(string path)
        {
            _db = new SQLiteAsyncConnection(path);

            // ایجاد جدول‌ها
            _db.CreateTableAsync<User>().Wait();
            _db.CreateTableAsync<Course>().Wait();
            _db.CreateTableAsync<Lesson>().Wait();
            _db.CreateTableAsync<Comment>().Wait();
            _db.CreateTableAsync<CartItem>().Wait();
            _db.CreateTableAsync<PurchasedCourse>().Wait();
        }

        // ================= USERS =================
        public Task<User?> GetUserAsync(string username, string password)
        {
            return _db.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveUserAsync(User user)
        {
            var existingUser = await _db.Table<User>()
                .Where(u => u.Username == user.Username)
                .FirstOrDefaultAsync();

            if (existingUser != null)
                return 0;

            return await _db.InsertAsync(user);
        }

        // ================= COURSES =================
        public Task<List<Course>> GetCoursesAsync()
            => _db.Table<Course>().ToListAsync();

        public Task<int> SaveCourseAsync(Course course)
            => _db.InsertAsync(course);

        public Task<Course?> GetCourseByTitleAsync(string title)
        {
            return _db.Table<Course>()
                .Where(c => c.Title == title)
                .FirstOrDefaultAsync();
        }

        public Task<int> UpdateCourseAsync(Course course)
            => _db.UpdateAsync(course);

        // ================= LESSONS =================
        public Task<List<Lesson>> GetLessonsAsync(int courseId)
        {
            return _db.Table<Lesson>()
                .Where(l => l.CourseId == courseId)
                .ToListAsync();
        }

        public Task<int> SaveLessonAsync(Lesson lesson)
            => _db.InsertAsync(lesson);

        // ================= COMMENTS =================
        public Task<List<Comment>> GetCommentsAsync(int courseId)
        {
            return _db.Table<Comment>()
                .Where(c => c.CourseId == courseId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public Task<int> SaveCommentAsync(Comment comment)
            => _db.InsertAsync(comment);

        // ================= CART =================
        public Task<List<CartItem>> GetCartItemsAsync()
            => _db.Table<CartItem>().ToListAsync();

        public Task<int> AddToCartAsync(CartItem item)
            => _db.InsertAsync(item);

        public Task<int> ClearCartAsync()
            => _db.DeleteAllAsync<CartItem>();

        // ================= PURCHASE =================
        public Task<int> SavePurchasedCourseAsync(int courseId)
        {
            return _db.InsertAsync(new PurchasedCourse
            {
                CourseId = courseId
            });
        }

        public async Task<List<Course>> GetPurchasedCoursesAsync()
        {
            var purchasedIds = (await _db.QueryAsync<PurchasedCourse>(
       "SELECT CourseId FROM PurchasedCourse"
        )).Select(p => p.CourseId).ToList();

            if (purchasedIds.Count == 0)
                return new List<Course>();

            return await _db.Table<Course>()
                .Where(c => purchasedIds.Contains(c.Id))
                .ToListAsync();
        }
    }
}
