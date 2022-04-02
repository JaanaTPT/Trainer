using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class TrainingExerciseEditModel
    {
        public int ID { get; set; }
        public int TrainingID { get; set; }
        public int ExerciseID { get; set; }
        public int Rounds { get; set; }
        public int Repetitions { get; set; }
        [Display(Name = "Max weight")]
        public int MaxWeight { get; set; }
        public String Comments { get; set; }

        public Training Training { get; set; }
        public Exercise Exercise { get; set; }
    }
}
