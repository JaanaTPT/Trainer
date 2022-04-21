using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public class Client : Entity
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Start weight")]
        public int StartWeight { get; set; }
        [Display(Name = "Current weight")]
        public int CurrentWeight { get; set; }
        public int Height { get; set; }

        [Display(Name = "Additional info")]
        public String AdditionalInfo { get; set; }
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        public ICollection<Training> Trainings { get; set; }
        public ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
