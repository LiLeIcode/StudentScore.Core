using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StudentScore.Models
{
    public class StudentScoreContext:DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddFilter((category,level) =>
            
                category == DbLoggerCategory.Database.Command.Name&&level==LogLevel.Information
            ).AddConsole(); });

        public DbSet<StudentInfo> StudentInfo { get; set; }
        public DbSet<ReportCard> ReportCard { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(@"Data Source=.;Initial Catalog=StudentScoreSqlServerCore;Integrated Security=True;uid=sa;pwd=root");
        

        //public StudentScoreContext(DbContextOptions<DbContext> options):base(options)
        //{
            
        //}
    }
}
