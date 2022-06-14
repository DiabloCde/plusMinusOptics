using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Interfaces
{
    public interface IExerciseRepository
    {
        List<Exercise> GetExercises(Expression<Func<Exercise, bool>> filter);

        Exercise GetExerciseByID(int exerciseId);

        void InsertExercise(Exercise exercise);

        void DeleteExercise(int exerciseId);

        void UpdateExercise(Exercise exercise);
    }
}
