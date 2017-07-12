using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TinyMceDemo.Models
{
    public class ContextClass : DbContext
    {
        public ContextClass()
            : base("name=QuestionDBEntities")
        {
            Database.SetInitializer<ContextClass>(null);
        }

        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}