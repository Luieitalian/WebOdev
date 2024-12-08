using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class OnayliRandevuModel
    {
        [Key]
        public int RandevuId { get; set; }

        // Navigation Property
        public RandevuModel Randevu { get; set; }
    }
}
