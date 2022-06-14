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
            try
            {
                Exercise exercise = this._context.Exercises.Find(exerciseId);
                this._context.Exercises.Remove(exercise);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Exercise GetExerciseByID(int exerciseId)
        {
            try
            {
                return this._context.Exercises.Find(exerciseId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Exercise> GetExercises(Expression<Func<Exercise, bool>> filter)
        {
            try
            {
                return this._context.Exercises.Where(filter).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertExercise(Exercise exercise)
        {
            try
            {
                this._context.Exercises.Add(exercise);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateExercise(Exercise exercise)
        {
            try
            {
                this._context.Entry(exercise).State = EntityState.Modified;
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
