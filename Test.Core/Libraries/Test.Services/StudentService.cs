using System;
using System.Collections.Generic;
using System.Linq;
using Test.Core.Data;
using Test.Core.Domain.Student;

namespace Test.Services
{
    public class StudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        public IList<Student> GetStudents()
        {
            return _studentRepository.Table.ToList();
        }
    }
}
