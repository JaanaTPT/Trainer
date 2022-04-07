using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class TrainingExerciseModel
    {
        public int ID { get; set; }
        public int TrainingID { get; set; }
        public int ExerciseID { get; set; }
        [Display(Name = "Exercise")]
        public string ExerciseName { get; set; }
        [Display(Name = "Client")]
        public string ClientName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Training date")]
        public DateTime TrainingDate { get; set; }
        public int Rounds { get; set; }
        public int Repetitions { get; set; }
        [Display(Name = "Max weight")]
        public int MaxWeight { get; set; }
        public String Comments { get; set; }

        public ICollection<Training> Trainings { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
