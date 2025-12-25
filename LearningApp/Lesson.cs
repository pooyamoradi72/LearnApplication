using SQLite;

namespace LearningApp
{
    public class Lesson
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; } // ربط به دوره
        public string Title { get; set; } = string.Empty;
        public string DownloadLink { get; set; } = string.Empty;
    }
}
