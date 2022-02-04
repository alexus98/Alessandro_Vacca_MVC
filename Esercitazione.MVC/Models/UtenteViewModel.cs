using System.ComponentModel.DataAnnotations;

namespace Esercitazione.MVC.Models
{
    public class UtenteViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
