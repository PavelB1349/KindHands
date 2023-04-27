using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KindHands.Models
{
    public class User : Entity
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Введите имя пользователя.")]
        [Display(Name = "Имя пользователя")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Имя пользователя должно быть в диапазоне {2}-{1}")]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Введите пароль.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть в диапазоне {2}-{1}")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть в диапазоне {2}-{1}")]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ConfirmPassword { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Укажите город.")]
        [Display(Name = "Город")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Количество символов должно быть в диапазоне {2}-{1}")]
        public string City { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Укажите номер телефона.")]
        [Display(Name = "Телефон")]
        //[Phone(ErrorMessage = "Телефон указан неверно.")]
        public string Phone { get; set; }

        [EmailAddressAttribute(ErrorMessage = "Email введен неверно.")]
        public string Email { get; set; }
        [Display(Name = "Администратор")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Модератор")]

        public bool IsModerator { get; set; }
        public ICollection<Animal> Animals { get; set; } = new HashSet<Animal>();
        public ICollection<Advertisement> Advertisements { get; set; } = new HashSet<Advertisement>();
        public ICollection<VolunterAnnouncement> VolunterAnnouncements { get; set; } = new HashSet<VolunterAnnouncement>();
    }
}
