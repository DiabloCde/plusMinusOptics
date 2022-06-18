using PlusMinus.Core.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Eyecare : Product
    {
        public int Volume { get; set; }

        public EyecarePurpose Purpose { get; set; }
    }
}
