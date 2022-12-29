using Catalog.Entities;

namespace Catalog.Repositorys
{
       public interface IItemRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();

        void CreateItem(Item item);

        void UpdateIten(Item item);

        void DeleteItem(Guid id);
    }
}