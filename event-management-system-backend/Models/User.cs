using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace event_management_system_backend.Models
{
    public class User
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public bool Reminder { get; set; }

        
        public List<Event> EventsAttending { get; set; } = new();

    }
}
