using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSHCTwebApp.Components.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Like> Likes { get; set; } = new();
    }
}
