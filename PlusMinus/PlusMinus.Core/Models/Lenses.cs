using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Lenses : Product
    {
        public double Dioptre { get; set; }

        public TimeSpan ExpirationDate { get; set; }
    }
}
