using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Models
{
    public class Training
    {
        public int TrainingID { get; set; }
        public DateTime Date { get; set; }
        public int ClientID { get; set; }

        public Client Client { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
