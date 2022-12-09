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
         this.repository =  _repository;   
         
    }

    [HttpGet]
    public IEnumerable<Item> GetItems()
    {
        var item = repository.GetItems();
        return item;
    }

    [HttpGet("{id}")]
    public ActionResult<Item> GetItem(Guid id){
        var item = repository.GetItem(id);

     if(item is null){
        return NotFound();
    }

        
        return item;
    }


    }
    
}