using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VSHCTwebApp.Components.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название команды обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 100 символов")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string Description { get; set; }

        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}