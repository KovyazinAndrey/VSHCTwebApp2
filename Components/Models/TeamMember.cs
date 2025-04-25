namespace VSHCTwebApp.Components.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public int CommandId { get; set; }
        public Command Command { get; set; }
    }
}
