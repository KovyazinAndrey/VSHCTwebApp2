using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VSHCTwebApp.Data;

namespace VSHCTwebApp.Components.Models;

public class UserCompetencies
{
    public int Id { get; set; }
    
    [Required]
    public required string UserId { get; set; }
    
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }
    
    public required string ProgrammingLanguages { get; set; }
    public required string Frameworks { get; set; }
    public required string Databases { get; set; }
    public required string DevOps { get; set; }
} 