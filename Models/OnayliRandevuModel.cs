using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebOdev.Models
{
    public class OnayliRandevuModel
    {
        [Key, ForeignKey("Randevu")] // Primary Key
        public int RandevuId { get; set; }

        // Navigation Property
        public RandevuModel Randevu { get; set; }
    }
}
