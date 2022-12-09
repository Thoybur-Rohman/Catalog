using Catalog.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Repositorys
{

    public class InMenuItemRepository : IItemRepository
    {
        private readonly List<Item> items = new()
        {
                new Item { Id = Guid.NewGuid() , Name = "Potion" , Price = 9 , CreatedDate = DateTimeOffset.UtcNow},
                new Item { Id = Guid.NewGuid() , Name = "Elexir" , Price = 10 , CreatedDate = DateTimeOffset.UtcNow},
                new Item { Id = Guid.NewGuid() , Name = "Wizard" , Price = 91 , CreatedDate = DateTimeOffset.UtcNow}
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }
    }
}