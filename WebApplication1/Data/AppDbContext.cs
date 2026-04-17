using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Incident> Incidents { get; set; }

        /*
         By default, EF Core tries to guess your database names. If you have a C# class named Incident, it will look for a database table named Incident.

            But databases usually use different naming styles (like incidents with a lowercase 'i', or created_at instead of CreatedAt). This code stops EF Core from guessing and gives it strict, explicit instructions. 

             Wat is DB Migration = instructions to update your database ........code → DB sync tool
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("incidents");
                //Each entity type in your model has a set of properties, which EF Core will read and write from the database. If you're using a relational database, entity properties map to table columns.
                entity.Property(e => e.Id).HasColumnName("id");//“Map this property to THIS column name”
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.State).HasColumnName("state");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                // 🔥 THIS FIXES YOUR ISSUE
                entity.HasOne(e => e.User)
                      .WithMany() // or .WithMany(u => u.Incidents) if you add that
                      .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
            });
        }
    }
}