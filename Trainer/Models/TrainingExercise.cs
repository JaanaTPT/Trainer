using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public class TrainingExercise
    {
        public int TrainingExerciseID { get; set; }
        public int TrainingID { get; set; }
        public int ExerciseID { get; set; }
        public int Rounds { get; set; }
        public int Repetitions { get; set; }
        [Display(Name = "Max weight")]
        public int MaxWeight { get; set; }
        public String Comments { get; set; }

        public Training Training { get; set; }
        public Exercise Exercise { get; set; }

        public string TrainingInfo
        {
            get
            {
                return Training.Date + " " + Training.Client.FullName;
            }
        }

    }
}
