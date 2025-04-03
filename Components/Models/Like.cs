using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using VSHCTwebApp.Data;

namespace VSHCTwebApp.Components.Models
{
    public class Like
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Note")]
        public int NoteId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        public virtual ApplicationUser User { get; set; }
        public virtual Note Note { get; set; }
    }
}
