using Microsoft.Extensions.Logging;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }


        public void AddExerciseToTimetable(Timetable timetable)
        {
            if (timetable is null)
            {
                throw new ArgumentException("Timetable cannot be null.");
            }

            try
            {
                this._userRepository.AddExerciseToTimetable(timetable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Timetable for user with id {UserId} was not added.", timetable.UserId);
                throw;
            }
        }

        public void DeleteExerciseFromTimetable(Timetable timetable)
        {
            if (timetable is null)
            {
                throw new ArgumentException("Timetable cannot be null.");
            }

            try
            {
                this._userRepository.DeleteExerciseFromTimetable(timetable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Timetable for user with id {UserId} was not found or already deleted.", timetable.UserId);
                throw;
            }
        }

        public void UpdateExerciseInTimetable(Timetable timetable)
        {
            if (timetable is null)
            {
                throw new ArgumentException("Timetable cannot be null.");
            }

            try
            {
                this._userRepository.UpdateExerciseInTimetable(timetable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Timetable for user with id {UserId} was not found or already updated.", timetable.UserId);
                throw;
            }
        }
    }
}
