using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSHCTwebApp.Components.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Проблема обязательна")]
        public string Problem { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ожидаемый результат обязателен")]
        public string Result { get; set; } = string.Empty;
        public string DescrNeededResurses { get; set; } = string.Empty;
        public string StackTech { get; set; } = string.Empty;

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Like> Likes { get; set; } = new();
    }
}
