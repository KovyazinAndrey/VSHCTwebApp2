using System.ComponentModel.DataAnnotations.Schema;

namespace VSHCTwebApp.Components.Models
{
    public class Competence
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public CompetenceCategory Category { get; set; }

        public virtual List<UserCompetence> UserCompetences { get; set; } = new();
    }

    public enum CompetenceCategory
    {
        ProgrammingLanguage,
        Framework,
        Database,
        DevOps
    }
}
