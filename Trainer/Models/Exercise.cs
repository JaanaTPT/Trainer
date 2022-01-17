using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trainer.Models
{
    public enum MuscleGroup
    {
        Abdominals, Arms, Back, Chest, Legs, Shoulders
    }
    public class Exercise : Entity
    {
        //public int ExerciseID { get; set; }
        [Required]
        [Display(Name = "Exercise name")]
        public string Title { get; set; }
        [Display(Name = "Muscle group")]
        public MuscleGroup? MuscleGroup { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }

    }
}
