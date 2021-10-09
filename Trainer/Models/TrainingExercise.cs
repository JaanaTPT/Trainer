﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Models
{
    public class TrainingExercise
    {
        public int TrainingExerciseID { get; set; }
        public int TrainingID { get; set; }
        public int ExerciseID { get; set; }
        public int Rounds { get; set; }
        public int Repetitions { get; set; }
        public int MaxWeight { get; set; }
        public String Comments { get; set; }

        public Training Training { get; set; }
        public Exercise Exercise { get; set; }
    }
}
