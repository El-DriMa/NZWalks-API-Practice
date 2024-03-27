using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?>GetByIdAsync(Guid Id);
        Task<Region?> GetByCodeAsync(string code);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid Id,Region region);

        Task<Region?> DeleteAsync(Guid Id);
    }
}

