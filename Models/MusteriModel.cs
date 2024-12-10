using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class MusteriModel
    {
        [Key]
        public string? KullaniciId { get; set; }
        public KullaniciModel Kullanici { get; set; }
    }
}
