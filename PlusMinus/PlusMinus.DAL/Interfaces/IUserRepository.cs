﻿using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Interfaces
{
    public interface IUserRepository
    {
        void AddExerciseToTimetable(Timetable timetable);

        void DeleteExerciseFromTimetable(Timetable timetable);

        void UpdateExerciseInTimetable(Timetable timetable);
    }
}
