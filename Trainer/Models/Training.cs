using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public class Training : Entity
    {
        [DataType(DataType.Date)]
        [Display(Name = "Training date")]
        public DateTime Date { get; set; }

        public Client Client { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
