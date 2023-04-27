using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KindHands.Models
{
    public class Veterinarian : Entity
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя пользователя должно быть в диапазоне {2}-{1} символов.")]
        public string FirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Введите Фамилию.")]
        [Display(Name = "Фамилия")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть в диапазоне {2}-{1} символов.")]
        public string LastName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Введите отчество.")]
        [Display(Name = "Отчество")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Отчество должно быть в диапазоне {2}-{1} символов.")]
        public string PatronumicName { get; set; }

        [Display(Name = "Специализация")]
        public string Speciality { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Укажите город.")]
        [Display(Name = "Город")]
        [StringLength(25, MinimumLength = 2)]
        public string City { get; set; }

        [Display(Name = "Телефон")]
        //[Phone(ErrorMessage = "Телефон указан неверно.")]
        public string Phone { get; set; }

        [EmailAddressAttribute(ErrorMessage = "Email введен неверно.")]
        public string Email { get; set; }

        // добивил путь файла
        public string? PhotoPath { get; set; }

        public int ClinicId { get; set; }

        public virtual Clinic? Clinic { get; set; }
    }
}
