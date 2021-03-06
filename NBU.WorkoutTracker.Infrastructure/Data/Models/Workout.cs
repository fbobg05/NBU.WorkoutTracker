﻿using System;
using System.Collections.Generic;
using NBU.WorkoutTracker.Infrastructure.Identity;

namespace NBU.WorkoutTracker.Infrastructure.Data.Models
{

    /// <summary>
    /// Contains what exercises should be done during a given workout session.
    /// </summary>
    public class Workout
    {
        public int WorkoutId { get; set; }

        public DateTime DateCreated { get; set; }

        public string WorkoutName { get; set; }

        public string WorkoutDetails { get; set; }

        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        public ICollection<CompletedWorkout> CompletedWorkouts { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
