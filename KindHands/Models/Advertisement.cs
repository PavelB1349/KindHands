using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KindHands.Models
{
    public class Advertisement : Entity
    {
        [Display(Name = "Описание")]
        [StringLength(2500, ErrorMessage = "Описание не должно превышать {1} символов")]
        public string Description { get; set; }
        public int UserId { get; set; }
        public int AnimalId { get; set; }
        public virtual User? User { get; set; }
        public virtual Animal? Animal { get; set; }
    }
}
