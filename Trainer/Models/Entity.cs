using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Models
{
    public abstract class Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
