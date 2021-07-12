using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Context
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(e => e.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Profilling)
                .WithOne(p => p.account)
                .HasForeignKey<Profilling>(p => p.NIK);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRole)
                .WithOne(ar => ar.Account);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRole);

            modelBuilder.Entity<Profilling>()
                .HasOne(p => p.education)
                .WithMany(edu => edu.Profilling);

            modelBuilder.Entity<Education>()
                .HasOne(edu => edu.University)
                .WithMany(u => u.education);
        }



    }
}
