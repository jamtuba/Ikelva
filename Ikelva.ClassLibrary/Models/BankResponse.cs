using System.ComponentModel.DataAnnotations;

namespace Ikelva.ClassLibrary.Models
{
    public class BankResponse
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public bool Response { get; set; }
    }
}
