using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ClientModel
    {
        public int ID { get; set; }
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
        [Display(Name = "Weight change")]
        public int WeightChange
        {
            get
            {
                return CurrentWeight - StartWeight;
            }
        }

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;

                if (today.Month < DateOfBirth.Month || (today.Month == DateOfBirth.Month && today.Day < DateOfBirth.Day))
                    age--;
                return age;
            }
        }

        public ICollection<Training> Trainings { get; set; }
        public ICollection<TrainingExercise> TrainingExercises { get; set; }
    }
}
