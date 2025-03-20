using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_management_system_backend.Models
{
    public class Event
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? Location { get; set; }

        [ForeignKey("CreatedBy")]
        public Guid CreatedByUserId { get; set; }
        public User? CreatedBy { get; set; }

        
        public List<User> Attendees { get; set; } = new();

    }
}
