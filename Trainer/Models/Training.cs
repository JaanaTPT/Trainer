using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public class Training : Entity
    {
        //public int TrainingID { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Training date")]
        public DateTime Date { get; set; }
        //public int ClientID { get; set; }

        public Client Client { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
