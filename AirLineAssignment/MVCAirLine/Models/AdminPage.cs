using System.ComponentModel.DataAnnotations;

namespace MVCAirLine.Models
{
    public class AdminPage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string PanNo { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MinLength(20)]
        [MaxLength(30)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(20)]
        public string RoleName { get; set; }

    }
}
