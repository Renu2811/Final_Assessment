using System.ComponentModel.DataAnnotations;

namespace MVCAirLine.Models
{
    public class Mapper
    {
        public int AirLineId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "AirLineName Should not be more than 50 char")]
        public string AirLineName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "FromCity Should not be more than 30 char")]
        public string FromCity { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "ToCity Should not be more than 30 char")]
        public string ToCity { get; set; }

        public int Fare { get; set; }

        [Display(Name = "Image")]
        public string AirLineImage { get; set; }
    }
}
