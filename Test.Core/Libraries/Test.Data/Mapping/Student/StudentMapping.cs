using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Test.Data.Mapping.Student
{
    public  class StudentMapping: EntityTypeBuilder<Core.Domain.Student.Student>
    {
        public StudentMapping(InternalEntityTypeBuilder builder) : base(builder)
        {
            this.ToTable("BlogPost");
            this.HasKey(bp => bp.Id);
            this.Property(bp => bp.Sid).IsRequired();
            this.Property(bp => bp.Name).IsRequired();
            this.Property(bp => bp.Class).IsRequired();
        }
    }
}
