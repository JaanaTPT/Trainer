using System;
using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public class TrainingExercise : Entity
    {
        public int Rounds { get; set; }
        public int Repetitions { get; set; }
        [Display(Name = "Max weight")]
        public int MaxWeight { get; set; }
        public String Comments { get; set; }

        public Training Training { get; set; }
        public Exercise Exercise { get; set; }
    }
}
