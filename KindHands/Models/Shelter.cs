using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KindHands.Models
{
    public class Shelter : Entity
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Введите название.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Название должно состоять в диапазоне {2}-{1} символов.")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Укажите город.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Название города должно состоять в диапазоне {2}-{1} символов.")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Укажите адрес.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Неверная длина строки")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email указан неверно.")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(8000, MinimumLength = 10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        // добивил путь файла
        public string? PhotoPath { get; set; }

        public virtual ICollection<Animal>? Animals { get; set; }
         = new HashSet<Animal>();
    }
}
