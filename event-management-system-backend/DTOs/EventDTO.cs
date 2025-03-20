namespace event_management_system_backend.DTOs
{
    public class EventDTO
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
    }
}
