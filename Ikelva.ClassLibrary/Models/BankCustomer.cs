using System.ComponentModel.DataAnnotations;

namespace Ikelva.ClassLibrary.Models
{
    public class BankCustomer
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
