﻿using Microsoft.AspNetCore.Mvc;
using MongoConnection.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApi.Data;
using WebApi.Data.Enum;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkoutController : ControllerBase
    {
        private readonly CrudRepository<Workout> _repository;

        public WorkoutController(CrudRepository<Workout> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult InsertWorkout(Workout workout)
        {
            try
            {
                _repository.InsertWorkout(workout);
                return Ok("Workout inserted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkoutById(string id)
        {
            try
            {
                var workout = _repository.FindById(x => x.Id == id);
                if (workout == null)
                    return NotFound("Workout not found.");
                
                return Ok(workout);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllWorkouts()
        {
            try
            {
                var workouts = _repository.GetAllXmls();
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorkout(string id, Workout workout)
        {
            try
            {
                Expression<Func<Workout, bool>> filter = x => x.Id == id;
                var existingWorkout = _repository.FindBy(filter);
                if (existingWorkout == null)
                {
                    return NotFound("Workout not found.");
                }

                _repository.UpdateWorkout(filter, workout);
                return Ok("Workout updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkout(string id)
        {
            try
            {
                Expression<Func<Workout, bool>> filter = x => x.Id == id;
                var existingWorkout = _repository.FindBy(filter);
                if (existingWorkout == null)
                {
                    return NotFound("Workout not found.");
                }

                _repository.RemoveWorkout(filter);
                return Ok("Workout deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
