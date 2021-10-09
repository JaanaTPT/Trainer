using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Models
{
    public enum MuscleGroup
    {
        Chest, Back, Arms, Abdominals, Legs, Shoulders
    }
    public class Exercise
    {
        public int ExerciseID { get; set; }
        public string Title { get; set; }
        public MuscleGroup? MuscleGroup { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }

    }
}
