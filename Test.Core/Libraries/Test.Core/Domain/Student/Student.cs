using System.ComponentModel.DataAnnotations;

namespace Test.Core.Domain.Student
{
    public class Student:BaseEntity
    {
        [Key]
        public int Sid { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }
    }
}
