using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLDifficultyRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await dbContext.Difficulty.ToListAsync();
        }

        public async Task<Difficulty?> GetByIdAsync(Guid Id)
        {
            return await dbContext.Difficulty.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await dbContext.Difficulty.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid Id)
        {
            var difficulty = dbContext.Difficulty.FirstOrDefault(x => x.Id == Id);
            if (difficulty == null) return null;
            dbContext.Remove(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }


        public async Task<Difficulty> UpdateAsync(Guid id, Difficulty difficulty)
        {
            var existingDifficulty = await dbContext.Difficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDifficulty == null) return null;

            existingDifficulty.Name = difficulty.Name;
            
            await dbContext.SaveChangesAsync();
            return existingDifficulty;

        }
    }
}
