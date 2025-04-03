using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VSHCTwebApp.Data;
using VSHCTwebApp.Components.Models;

namespace VSHCTwebApp.Components.Services;

public class CompetenciesService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<CompetenciesService> _logger;

    public CompetenciesService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        ILogger<CompetenciesService> logger)
    {
        _context = context;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<UserCompetencies?> GetUserCompetenciesAsync(ApplicationUser user)
    {
        try
        {
            _logger.LogInformation($"Getting competencies for user {user.Id}");
            return await _context.UserCompetencies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting competencies for user {user.Id}");
            throw;
        }
    }

    public async Task<bool> SaveUserCompetenciesAsync(ApplicationUser user, UserCompetencies competencies)
    {
        try
        {
            _logger.LogInformation($"Saving competencies for user {user.Id}");
            
            var existingCompetencies = await _context.UserCompetencies
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            if (existingCompetencies != null)
            {
                _logger.LogInformation($"Updating existing competencies for user {user.Id}");
                _context.UserCompetencies.Remove(existingCompetencies);
            }

            competencies.UserId = user.Id;
            await _context.UserCompetencies.AddAsync(competencies);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully saved competencies for user {user.Id}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error saving competencies for user {user.Id}");
            return false;
        }
    }
} 