using NZWalks.api.Models.Domain;

namespace NZWalks.api.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
