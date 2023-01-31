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
        public async Task<ActionResult<ItemDto>> CreateItemasync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemasync), new { id = item.Id }, item.AsDtos());

        }

        // GET all Items 
        [HttpGet]
        public async Task<IEnumerable<ItemDto>>GetItems()
        {
            var item = (await repository.GetItemsAsync()).Select(item => item.AsDtos());
            return item;
        }


        //GET by Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemasync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }
            return item.AsDtos();
        }

        //PUT / items / {id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemasync(Guid id, UpdateDto updateItemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            await repository.UpdateItenAsync(updatedItem);
            return NoContent();
        }

        //Delete/item/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemasync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);
            return NoContent();
        }




    }

}