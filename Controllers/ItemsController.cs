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

        // Get all Items 
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var item = repository.GetItems().Select(item => item.AsDtos());
            return item;
        }


        //Get by Id 
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


        //Post 
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
                Item item = new(){
                    Id = Guid.NewGuid(),
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    CreatedDate = DateTimeOffset.UtcNow
                };

                repository.CreateItem(item);

                return CreatedAtAction(nameof(GetItem), new{ id = item.Id} , item.AsDtos());

        }

    }

}