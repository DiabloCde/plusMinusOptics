﻿using PlusMinus.Core.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Glasses : Product
    {
        public string Color { get; set; }

        public FrameForm Form { get; set; }

        public string Material { get; set; }

        public double? Dioptre { get; set; }
    }
}
