using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVCAirLine.Models
{
    public class Field : IdentityUser
    {
        [Required]
        public string PanNo { get; set; }
    }
}
