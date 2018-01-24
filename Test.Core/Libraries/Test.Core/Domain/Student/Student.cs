using System.ComponentModel.DataAnnotations;

namespace Test.Core.Domain.Student
{
    public class Student:BaseEntity
    {
        public string Name { get; set; }

        public string Class { get; set; }
    }
}
