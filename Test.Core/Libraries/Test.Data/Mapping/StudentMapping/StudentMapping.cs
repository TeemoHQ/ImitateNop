using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Test.Core.Domain;
using Test.Core.Domain.Student;

namespace Test.Data.Mapping.StudentMapping
{
    public  class StudentMapping : ISelfEntityMappingConfiguration
    {
        public void Map(ModelBuilder b)
        {
            b.Entity<Student>().ToTable("Student")
                .HasKey(p => p.Id);

            b.Entity<Student>().Property(p => p.Class).HasMaxLength(50).IsRequired();
            b.Entity<Student>().Property(p => p.Name).HasMaxLength(50);
        }
    }
}
