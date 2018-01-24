using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Test.Core.Domain
{
    public interface ISelfEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
    }
}
