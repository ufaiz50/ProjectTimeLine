using Microsoft.EntityFrameworkCore;
using ProjectTimeLine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TaskModul> TaskModuls { get; set; }
        public DbSet<Modul> Moduls { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<AccountTask> AccountTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Account)
                .WithOne(a => a.Employee)
                .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<AccountRole>().HasKey(sc => new { sc.NIK, sc.RoleID });

            modelBuilder.Entity<AccountRole>()
                .HasOne<Account>(sc => sc.Account)
                .WithMany(s => s.AccountRoles)
                .HasForeignKey(sc => sc.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne<Role>(sc => sc.Role)
                .WithMany(s => s.AccountRoles)
                .HasForeignKey(sc => sc.RoleID);
            
            modelBuilder.Entity<AccountTask>().HasKey(sc => new { sc.NIK, sc.TaskModulId });

            modelBuilder.Entity<AccountTask>()
                .HasOne<Account>(sc => sc.Account)
                .WithMany(s => s.AccountTasks)
                .HasForeignKey(sc => sc.NIK);

            modelBuilder.Entity<AccountTask>()
                .HasOne<TaskModul>(sc => sc.TaskModul)
                .WithMany(s => s.AccountTasks)
                .HasForeignKey(sc => sc.TaskModulId);

            modelBuilder.Entity<Account>()
                .HasMany(ed => ed.TaskHistories)
                .WithOne(u => u.Account);

            modelBuilder.Entity<TaskModul>()
                .HasMany(ed => ed.TaskHistories)
                .WithOne(u => u.TaskModul);

            modelBuilder.Entity<Modul>()
                .HasMany(ed => ed.TaskModuls)
                .WithOne(u => u.Modul);

            modelBuilder.Entity<Project>()
                .HasMany(ed => ed.Moduls)
                .WithOne(u => u.Project);
        }

    }
}
