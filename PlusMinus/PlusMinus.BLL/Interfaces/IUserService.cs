using PlusMinus.Core.Models;

namespace PlusMinus.BLL.Interfaces
{
    public interface IUserService
    {
        void AddExerciseToTimetable(Timetable timetable);

        void DeleteExerciseFromTimetable(Timetable timetable);

        void UpdateExerciseInTimetable(Timetable timetable);
    }
}
