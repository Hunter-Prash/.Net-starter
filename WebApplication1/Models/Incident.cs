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
        public User User { get; set; } // Navigation property to the User entity
    }
}
