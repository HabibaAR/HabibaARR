using System.ComponentModel.DataAnnotations;

namespace HabibaARR.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le prénom est requis")]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide")]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fonction est requise")]
        public string Function { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Le mot de passe doit contenir au minimum 8 caractères")]
        [DataType(DataType.Password)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un caractère spécial")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmation du mot de passe est requise")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
