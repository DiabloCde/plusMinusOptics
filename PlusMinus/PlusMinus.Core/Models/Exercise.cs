using PlusMinus.Core.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.Core.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public TimeSpan Duration { get; set; }

        public ExerciseType Type { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}
