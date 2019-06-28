using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.Models
{
    public class Employee
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя"), Required(ErrorMessage = "Имя является обязательным")]
        [RegularExpression(@"(^[А-ЯЁ][а-яё]{2,150}$)|(^[A-Z][a-z]{2,150}$)", ErrorMessage = "Некорректный формат имени")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия"), Required(ErrorMessage = "Фамилия является обязательной")]
        [MinLength(2)]
        public string SurName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 130)]
        public int Age { get; set; }
    }
}
