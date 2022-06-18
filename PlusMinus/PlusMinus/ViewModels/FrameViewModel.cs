using System.ComponentModel.DataAnnotations;
using PlusMinus.Core.Models.Enumerations;

namespace PlusMinus.ViewModels
{
    public class FrameViewModel
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

        public FrameForm Form { get; set; }

        [Required]
        public MaterialViewModel Material { get; set; }
    }
}