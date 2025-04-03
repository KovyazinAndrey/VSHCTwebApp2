using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VSHCTwebApp.Components.Models;
using VSHCTwebApp.Data;

namespace VSHCTwebApp.Components.Services
{
    public class LikeService
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthenticationStateProvider _authProvider;

        public LikeService(ApplicationDbContext context, AuthenticationStateProvider authProvider)
        {
            _context = context;
            _authProvider = authProvider;
        }

        public async Task ToggleLike(int postId)
        {
            var user = await GetCurrentUser();
            if (user == null) throw new UnauthorizedAccessException();

            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.NoteId == postId && l.UserId == user.Id);

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
            }
            else
            {
                var newLike = new Like { NoteId = postId, UserId = user.Id };
                _context.Likes.Add(newLike);
            }

            await _context.SaveChangesAsync();
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
