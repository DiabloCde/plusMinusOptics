using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
