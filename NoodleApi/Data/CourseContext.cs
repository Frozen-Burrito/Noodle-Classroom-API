using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using NoodleApi.Models;

namespace NoodleApi.Data
{
    // Uses Entity Framework Core to provide a relational database context
    // for the application. Can be used with SQLite or SQL Server.
    /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.CourseContext"]/*'/>
    public class CourseContext : DbContext
    {
        // Initializes the CourseContext instance, invokes base constructor 
        // DbContext(options).
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Data.CourseContext.Constructor"]/*'/>
        public CourseContext(DbContextOptions<CourseContext> options)
            : base(options)
        {
        }

        // A collection of courses.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="P:NoodleApi.Data.CourseContext.Courses"]/*'/>
        public DbSet<Course> Courses { get; set; }
        // A collection of publications.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="P:NoodleApi.Data.CourseContext.Publications"]/*'/>
        public DbSet<Publication> Publications { get; set; }

        // Called when creating the database schema. Configures models.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.CourseContext.OnModelCreating(ModelBuilder modelBuilder)"]/*'/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .ToTable("Course")
                .HasMany(c => c.Publications)
                .WithOne();
                
            modelBuilder.Entity<Publication>().ToTable("Publication");
        }
    }
}