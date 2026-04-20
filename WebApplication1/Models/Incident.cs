namespace WebApplication1.Models
{
    public class Incident
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } // Navigation property to the User class via the UserId foreign key. This allows you to access the related User entity when you query for an Incident.I have to add this property and write the foreign key relationship in the OnModelCreating method of the AppDbContext class to establish the relationship between the Incident and User entities.
    }
}
