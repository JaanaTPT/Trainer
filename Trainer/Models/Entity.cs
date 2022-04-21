using System.ComponentModel.DataAnnotations;

namespace Trainer.Models
{
    public abstract class Entity
    {
        [Key]
        public int ID { get; set; }
    }
}
