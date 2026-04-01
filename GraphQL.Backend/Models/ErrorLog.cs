namespace GraphQLDemo.Models
{
    public class ErrorLog
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public List<string> Message { get; set; }
        public string StackTrace { get; set; }
        public string Service { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
