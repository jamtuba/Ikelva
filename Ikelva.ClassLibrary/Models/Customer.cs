using System.ComponentModel.DataAnnotations;

namespace Ikelva.ClassLibrary.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FurnitureId { get; set; }

        [Required(ErrorMessage = "Husk fornavn")]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Husk efternavn")]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Husk addresse")]
        [Display(Name = "Addresse")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Husk email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Du skal indtaste en gyldig email adresse")]
        public string EmailAddress { get; set; }
    }
}
