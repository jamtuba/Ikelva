using System.ComponentModel.DataAnnotations;

namespace Ikelva.ClassLibrary.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FurnitureId { get; set; }

        [Required]
        [Display(Name = "Fornavn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Du skal indtaste en gyldig email adresse")]
        public string EmailAddress { get; set; }
    }
}
