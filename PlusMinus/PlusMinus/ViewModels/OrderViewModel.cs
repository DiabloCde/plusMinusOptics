using System;
using System.Collections.Generic;
using PlusMinus.Core.Models;
using PlusMinus.Core.Models.Enumerations;

namespace PlusMinus.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public string UserSurname { get; set; }

        public string UserName { get; set; }

        public string UserLastname { get; set; }

        public string UserAddress { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }

        public double SumPrice { get; set; }

        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}