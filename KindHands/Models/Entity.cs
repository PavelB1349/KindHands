using System.ComponentModel.DataAnnotations;

namespace KindHands.Models
{
    public abstract class Entity
    {
        public int Id { get; set; } = 0;
        [Display(Name = "Дата создания")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
