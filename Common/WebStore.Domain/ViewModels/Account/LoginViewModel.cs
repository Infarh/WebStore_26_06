using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Имя пользвоателя"), MaxLength(256, ErrorMessage = "Максимальная длина 256 символов")]
        public string UserName { get; set; }

        [Display(Name = "Пароль"), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
