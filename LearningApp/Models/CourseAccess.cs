using SQLite;

namespace LearningApp;

public class CourseAccess
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int CourseId { get; set; }
}
