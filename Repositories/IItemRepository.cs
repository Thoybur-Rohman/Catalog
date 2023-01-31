using Catalog.Entities;

namespace Catalog.Repositorys
{
       public interface IItemRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();

        Task CreateItemAsync(Item item);

        Task UpdateItenAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}