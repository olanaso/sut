using System.ComponentModel.DataAnnotations;

namespace Sut.Web.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        public string UserNameEncrypt { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int TipoDoc { get; set; }

        public string JavascriptToRun { get; set; }


        [Compare("PasswordNew", ErrorMessage = "Las constraseñas no son iguales")]
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string NombreCompleto { get; set; }
        public string DaysChangePassword { get; set; }

    }
}