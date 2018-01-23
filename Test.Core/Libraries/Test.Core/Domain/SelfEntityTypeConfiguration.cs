using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Test.Core.Domain
{
    public class SelfEntityTypeConfiguration<T> : EntityTypeBuilder<T> where T:class 
    {
        /// <summary>
        /// Ctor
        /// </summary>
        protected SelfEntityTypeConfiguration(InternalEntityTypeBuilder builder) : base(builder)
        {
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }
    }
}
