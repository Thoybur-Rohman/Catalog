using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record UpdateDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1,1000)]
        public int Price { get; init; }
    } 
}