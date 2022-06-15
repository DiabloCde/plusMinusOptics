using PlusMinus.Core.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }
    }
}
