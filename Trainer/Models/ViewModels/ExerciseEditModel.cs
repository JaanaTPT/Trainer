using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ExerciseEditModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Exercise name")]
        public string Title { get; set; }
        [Display(Name = "Muscle group")]
        public MuscleGroup? MuscleGroup { get; set; }
    }
}
