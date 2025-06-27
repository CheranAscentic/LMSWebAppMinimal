using LMSWebAppMinimal.Data.Configuration;
using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSWebAppMinimal.Data.Context
{
    public class DataDBContext : DbContext
    {
        public DataDBContext(DbContextOptions<DataDBContext> options) : base(options) {}

        public DbSet<Book> Books { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Member> Members { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Apply configuration classes
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BaseUserConfiguration());
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
        }
    }
}
