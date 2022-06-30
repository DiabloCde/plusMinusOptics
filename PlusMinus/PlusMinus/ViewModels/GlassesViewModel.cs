using Microsoft.AspNetCore.Http;
using PlusMinus.Core.Models.Enumerations;

namespace PlusMinus.ViewModels
{
    public class GlassesViewModel
    {
        public int? ProductId { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public string Color { get; set; }

        public FrameForm Form { get; set; }

        public MaterialViewModel Material { get; set; }

        public double? Dioptre { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }
    }
}