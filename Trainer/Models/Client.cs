using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int StartWeight { get; set; }
        public int CurrentWeight { get; set; }
        public int Height { get; set; }
        public String AdditionalInfo { get; set; }

        public ICollection<Training> Trainings { get; set; }
    }
}
