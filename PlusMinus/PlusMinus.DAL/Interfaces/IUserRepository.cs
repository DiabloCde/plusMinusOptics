using PlusMinus.Core.Models;
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
        List<User> GetUsers(Expression<Func<User, bool>> filter);
        User GetUserByID(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void AddExerciseToTimetable(Timetable timetable);
        void DeleteExerciseFromTimetable(Timetable timetable);
        void UpdateExerciseInTimetable(Timetable timetable);
    }
}
