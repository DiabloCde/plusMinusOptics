using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PlusMinus.ViewModels
{
    public class LensesViewModel
    {
        public int? ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        public string Dioptre { get; set; }

        public string BaseCurve { get; set; }

        public int NumberOfUnits { get; set; }

        public string Diameter { get; set; }
        
        public int ExpirationDateValue { get; set; }

        public string Price { get; set; }

        public int Amount { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }
    }
}