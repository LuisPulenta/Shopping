using Microsoft.AspNetCore.Identity;
using Shopping.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class User:IdentityUser
    {
        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; }

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; }

        [Display(Name = "Ciudad")]
        public City City { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public string? ImageId { get; set; }

        //TODO: Corregir ruta
        [Display(Name = "Foto")]
        public string? ImageFullPath => string.IsNullOrEmpty(ImageId)
             ? "https://localhost:7101/images/nouser.png"
            : $"https://localhost:7101{ImageId.Substring(1)}";
        //? "http://keypress.serveftp.net:99/ShoppingApi/Images/nouser.png"
        //: $"http://keypress.serveftp.net:99/ShoppingApi{ImageId.Substring(1)}";

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
        public ICollection<Sale> Sales { get; set; }
    }
}