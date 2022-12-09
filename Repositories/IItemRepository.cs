using Catalog.Entities;

namespace Catalog.Repositorys
{
       public interface IItemRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}