using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebOdev.Models
{
    public class CalisanModel
    {
        [Key]
        public string KullaniciId { get; set; }
        public KullaniciModel Kullanici { get; set; }

        [ValidateNever, JsonIgnore]
        public ICollection<CalisanIslemModel> CalisanIslemleri { get; set; }
    }
}
