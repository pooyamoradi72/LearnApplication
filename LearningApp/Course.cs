using SQLite;

namespace LearningApp
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Image { get; set; } = "";
        public string Price { get; set; } = "";
        public bool IsPurchased { get; set; }
       
    }
}
