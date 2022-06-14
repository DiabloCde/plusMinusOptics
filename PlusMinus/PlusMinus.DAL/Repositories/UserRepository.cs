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

        public void DeleteUser(int userId)
        {
            try
            {
                User user = this._context.Users.Find(userId);
                this._context.Users.Remove(user);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetUserByID(int userId)
        {
            try
            {
                return this._context.Users.Find(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> GetUsers(Expression<Func<User, bool>> filter)
        {
            try
            {
                return this._context.Users
                    .Include(t => t.Timetables)
                    .Include(t => t.Orders)
                    .Where(filter)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertUser(User user)
        {
            try
            {
                this._context.Users.Add(user);
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

        public void UpdateUser(User user)
        {
            try
            {
                this._context.Entry(user).State = EntityState.Modified;
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
