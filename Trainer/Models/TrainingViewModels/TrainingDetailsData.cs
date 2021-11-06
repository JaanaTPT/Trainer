using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Models.TrainingViewModels
{
    public class TrainingDetailsData
    {
        
            public IEnumerable<Training> Trainings { get; set; }
            public IEnumerable<TrainingExercise> TrainingExercises { get; set; }
            public IEnumerable<Exercise> Exercises { get; set; }

    }
}
