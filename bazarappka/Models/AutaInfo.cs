using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace bazarappka.Models
{
    public class AutaInfo
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Manufacturer { get; set; }
        [Required]
        [MaxLength(30)]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Mileage { get; set; }
        [Required]
        public FuelType Fuel { get; set; }
        [Required]
        public BodyType Body { get; set; }
        [Required]
        [MaxLength(10)]
        public string LicensePlate { get; set; }
        [Required]
        public ConditionType Condition { get; set; }
        public DateTime ListedSince { get; set; }
        public string? OtherDetails { get; set; }

    }
}
