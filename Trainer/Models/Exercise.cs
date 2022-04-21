using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public enum MuscleGroup
    {
        Abdominals, Arms, Back, Chest, Legs, Shoulders
    }
    public class Exercise : Entity
    {
        [Required]
        [Display(Name = "Exercise name")]
        public string Title { get; set; }
        [Display(Name = "Muscle group")]
        public MuscleGroup? MuscleGroup { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }

    }
}
