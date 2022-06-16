using Microsoft.EntityFrameworkCore;
using PlusMinus.Core.Models;
using PlusMinus.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public void AddExerciseToTimetable(Timetable timetable)
        {
            try
            {
                this._context.Timetables.Add(timetable);
                this._context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void DeleteExerciseFromTimetable(Timetable timetable)
        {
            try
            {
                Timetable time = this._context.Timetables
                    .Single(x => x.ExerciseId == timetable.ExerciseId 
                    && x.UserId == timetable.UserId);
                this._context.Timetables.Remove(time);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateExerciseInTimetable(Timetable timetable)
        {
            try
            {
                Timetable time = this._context.Timetables
                    .Single(x => x.ExerciseId == timetable.ExerciseId
                    && x.UserId == timetable.UserId);
                time.Time = timetable.Time;
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
