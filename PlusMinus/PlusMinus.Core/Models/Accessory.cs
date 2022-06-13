using PlusMinus.Core.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Accessory : Product
    {
        public string Color { get; set; }

        public string Material { get; set; }

        public AccessoryType AccessoryType { get; set; } 
    }
}
