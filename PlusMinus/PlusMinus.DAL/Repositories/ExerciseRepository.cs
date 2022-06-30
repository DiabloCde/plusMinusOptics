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
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationContext _context;

        public ExerciseRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public void DeleteExercise(int exerciseId)
        {
            Exercise exercise = this._context.Exercises.Find(exerciseId);
            this._context.Exercises.Remove(exercise);
            this._context.SaveChanges();
        }

        public Exercise FirstOrDefault(Expression<Func<Exercise, bool>> filter)
        {
            Exercise? exercise = this._context.Exercises.FirstOrDefault(filter);

            return exercise;
        }

        public Exercise GetExerciseByID(int exerciseId)
        {
            return this._context.Exercises.Find(exerciseId);
        }

        public IEnumerable<Exercise> GetExercises(Expression<Func<Exercise, bool>> filter)
        {
            return this._context.Exercises.Where(filter);
        }

        public void InsertExercise(Exercise exercise)
        {
            this._context.Exercises.Add(exercise);
            this._context.SaveChanges();
        }

        public void UpdateExercise(Exercise exercise)
        {
            this._context.Entry(exercise).State = EntityState.Modified;
            this._context.SaveChanges();
        }
    }
}
