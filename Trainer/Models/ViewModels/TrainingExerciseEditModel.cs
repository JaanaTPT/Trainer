using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public IList<SelectListItem> Trainings { get; set; }
        public IList<SelectListItem> Exercises { get; set; }
    }
}
