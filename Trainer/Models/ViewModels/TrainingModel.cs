using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class TrainingModel
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Training date")]
        public DateTime Date { get; set; }
        public int ClientID { get; set; }
        [Display(Name = "Client name")]
        public string ClientName { get; set; }

        public ICollection<TrainingExercise> TrainingExercises { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
