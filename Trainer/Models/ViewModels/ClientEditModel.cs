using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ClientEditModel
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
        [StringLength(20)]
        public String Phone { get; set; }
        [StringLength(50)]
        [Display(Name = "E-mail")]
        public String Email { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Start weight")]
        public int StartWeight { get; set; }
        [Display(Name = "Current weight")]
        public int CurrentWeight { get; set; }
        public int Height { get; set; }

        [Display(Name = "Additional info")]
        public String AdditionalInfo { get; set; }
    }
}
