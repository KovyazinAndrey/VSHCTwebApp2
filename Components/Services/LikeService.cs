using Microsoft.EntityFrameworkCore;
using VSHCTwebApp.Components.Models;
using VSHCTwebApp.Data;

namespace VSHCTwebApp.Components.Services
{
    public class LikeService
    {
        private readonly IDbContextFactory<VSHCTwebAppContext> _contextFactory;

        public LikeService(IDbContextFactory<VSHCTwebAppContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task ToggleLikeAsync(int noteId, string userId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var existingLike = await context.Likes
                .FirstOrDefaultAsync(l => l.NoteId == noteId && l.UserId == userId);

            if (existingLike != null)
            {
                context.Likes.Remove(existingLike);
            }
            else
            {
                context.Likes.Add(new Like { NoteId = noteId, UserId = userId });
            }

            await context.SaveChangesAsync();
        }

        public async Task<int> GetLikesCountAsync(int noteId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Likes.CountAsync(l => l.NoteId == noteId);
        }

        public async Task<bool> IsLikedByUserAsync(int noteId, string userId)
        {
            if (string.IsNullOrEmpty(userId)) return false;

            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Likes
                .AnyAsync(l => l.NoteId == noteId && l.UserId == userId);
        }

    }
}