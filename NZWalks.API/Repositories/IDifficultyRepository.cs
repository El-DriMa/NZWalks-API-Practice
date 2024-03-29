using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();
        Task<Difficulty?> GetByIdAsync(Guid Id);
        Task<Difficulty> CreateAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateAsync(Guid Id,Difficulty difficulty);
        Task<Difficulty?> DeleteAsync(Guid Id);
    }
}
