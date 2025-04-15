using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using VSHCTwebApp.Data;

namespace VSHCTwebApp.Components.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Note Note { get; set; }
    }
}
