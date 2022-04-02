using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Trainer.Models.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class TrainingEditModel
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Training date")]
        public DateTime Date { get; set; }
        public int ClientID { get; set; }
        public IList<SelectListItem> Manufacturers { get; set; }
    }
}
