using Microsoft.EntityFrameworkCore;
using Task___Time_Tracker_App.Models;

namespace Task___Time_Tracker_App.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) :base(opt)
         {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeLog>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade path issue

            modelBuilder.Entity<TimeLog>()
                .HasOne(t => t.Task)
                .WithMany()
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade); // This is okay
        }

        public DbSet<User> users { get; set; }  
        public DbSet<Tasks> tasks { get; set; }

        public DbSet<TimeLog> timeLogs { get; set; }
    }
   
    }
