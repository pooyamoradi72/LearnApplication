using SQLite;
using System;

namespace LearningApp
{
    public class PurchasedCourse1
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int CourseId { get; set; }
    }
}
