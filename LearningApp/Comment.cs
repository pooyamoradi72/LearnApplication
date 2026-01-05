using SQLite;

namespace LearningApp
{
    public class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string UserName { get; set; } = "کاربر";

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
