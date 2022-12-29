using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository repository;

        public ItemsController(IItemRepository _repository)
        {
            this.repository = _repository;

        }
        //POST 
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDtos());

        }

        // GET all Items 
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var item = repository.GetItems().Select(item => item.AsDtos());
            return item;
        }


        //GET by Id 
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }
            return item.AsDtos();
        }

        //PUT / items / {id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateDto updateItemDto)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            repository.UpdateIten(updatedItem);
            return NoContent();
        }

        //Delete/item/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);
            return NoContent();
        }




    }

}