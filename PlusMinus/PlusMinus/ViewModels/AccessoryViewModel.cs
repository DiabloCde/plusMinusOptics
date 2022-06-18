using System.ComponentModel.DataAnnotations;
using PlusMinus.Core.Models.Enumerations;

namespace PlusMinus.ViewModels
{
    public class AccessoryViewModel
    {
        public int? ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        [Required]
        public string Color { get; set; }
        
        public MaterialViewModel Material { get; set; }

        public AccessoryType AccessoryType { get; set; }
    }
}