using System;
using System.ComponentModel.DataAnnotations;

namespace PlusMinus.ViewModels
{
    public class LensesViewModel
    {
        public int? ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        public double Dioptre { get; set; }

        public double BaseCurve { get; set; }

        public int NumberOfUnits { get; set; }

        public double Diameter { get; set; }
        
        public int ExpirationDateValue { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}