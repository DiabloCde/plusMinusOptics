using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Timetable
    {
        public int TimetableId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public DateTime Time { get; set; }
    }
}
