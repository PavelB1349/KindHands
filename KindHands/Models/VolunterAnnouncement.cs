using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KindHands.Models
{
    public class VolunterAnnouncement : Entity
    {
        [Required]
        [Display(Name = "Описание")]
        [StringLength(2500, ErrorMessage = "Описание должно состоять из {1} символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите город.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Название города должно состоять в диапазоне {2}-{1} символов.")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Телефон")]        
        public string Phone { get; set; }
            
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email указан неверно.")]
        public string Email { get; set; }

        // добивил путь файла
        public string? PhotoPath { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
