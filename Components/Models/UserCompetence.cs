using System.ComponentModel.DataAnnotations.Schema;
using VSHCTwebApp.Data;

namespace VSHCTwebApp.Components.Models
{
    public class UserCompetence
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int CompetenceId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [ForeignKey("CompetenceId")]
        public virtual Competence? Competence { get; set; }
    }
}
