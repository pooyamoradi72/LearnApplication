using SQLite;

namespace LearningApp
{
    public class CartItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseImage { get; set; }
    }
}
