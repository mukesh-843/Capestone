using event_management_system_backend.Models;
using Microsoft.EntityFrameworkCore;
namespace event_management_system_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define Many-to-Many relationship between Events and Attendees
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Attendees)
                .WithMany(u => u.EventsAttending)
                .UsingEntity(j => j.ToTable("EventAttendees"));

            // Define One-to-Many relationship: An event is created by a single user
            modelBuilder.Entity<Event>()
                .HasOne(e => e.CreatedBy)
                .WithMany()  // No navigation property in User for created events
                .HasForeignKey(e => e.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete issues
        }

    }
}
