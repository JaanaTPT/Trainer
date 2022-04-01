using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    public enum MuscleGroup
    {
        Abdominals, Arms, Back, Chest, Legs, Shoulders
    }

    [ExcludeFromCodeCoverage]
    public class ExerciseModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Exercise name")]
        public string Title { get; set; }
        [Display(Name = "Muscle group")]
        public MuscleGroup? MuscleGroup { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
