using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Test.Core;
using Test.Core.Domain;

namespace Test.Data
{
    public class SelfDbContext: DbContext,IDbContext
    {
        public SelfDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType&&
                               type.BaseType.GetGenericTypeDefinition() == typeof(SelfEntityTypeConfiguration<>));

            //var a = Assembly.GetExecutingAssembly().GetTypes();
            //var b = a.Where(type => !string.IsNullOrEmpty(type.Namespace));
            //foreach (var type in b)
            //{
            //    var c1 = type.BaseType;
            //    var c2 = type.BaseType.GetGenericTypeDefinition();
            //    var c3 = typeof(SelfEntityTypeConfiguration<>);
            //}


            foreach (var type in typesToRegister)
            {
                modelBuilder.Entity(type);
            }
            //base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public bool ProxyCreationEnabled { get; set; }
        public bool AutoDetectChangesEnabled { get; set; }
    }
}
