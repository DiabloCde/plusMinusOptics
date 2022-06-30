using Microsoft.AspNetCore.Http;
using PlusMinus.Core.Models.Enumerations;

namespace PlusMinus.ViewModels
{
    public class EyecareViewModel
    {
        public int? ProductId { get; set; }

        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

        public string Brand { get; set; }

        public string Price { get; set; }

        public int Amount { get; set; }

        public int Volume { get; set; }

        public EyecarePurpose Purpose { get; set; }
    }
}