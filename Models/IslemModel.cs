using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebOdev.Models
{
    public class IslemModel
    {
        [Key]
        public int? Id { get; set; }
        public string Baslik { get; set; }
        public string? Aciklama { get; set; }

        public string? ImageURL { get; set; }

        [Required]
        public TimeSpan Uzunluk { get; set; }
        public int Ucret { get; set; }

        public CinsiyetEnum Cinsiyet { get; set; }

        [ValidateNever, JsonIgnore]
        public ICollection<CalisanIslemModel> CalisanIslemleri { get; set; }
    }

    
}
