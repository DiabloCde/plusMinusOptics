using Microsoft.AspNetCore.Identity;
using PlusMinus.Core.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Address { get; set; }

        public double SightLeft { get; set; }

        public double SightRight { get; set; }

        public string Receipt { get; set; }

        public List<Order> Orders { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}
