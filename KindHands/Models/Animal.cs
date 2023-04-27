using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace KindHands.Models
{
    public class Animal : Entity
    {
        [Display(Name = "Имя")]
        public string? Name { get; set; }

        [Display(Name = "Возраст")]
        [Range(0, 30, ErrorMessage = "Возраст должен быть в диапазоне {1}-{2}")]
        public int? Age { get; set; }

        [Display(Name = "Порода")]
        public string? Breed { get; set; }

        [Display(Name = "Вид")]
        public Kind Kind { get; set; }

        [Display(Name = "Паспорт")]
        public Passport Passport { get; set; }

        [Display(Name = "Пол")]
        public Sex Sex { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; }
                     
        public string? PhotoPath { get; set; }

        public virtual ICollection<Advertisement>? Advertisements { get; set; }
            = new HashSet<Advertisement>();

        public virtual ICollection<Shelter>? Shelters { get; set; }
            = new HashSet<Shelter>();
    }

    public enum Kind
    {
        cat,
        dog,
        otherAnimal
    }

    public enum Sex
    {
        мужской, женский
    }
    public enum Passport
    {
        есть, нет
    }
}
