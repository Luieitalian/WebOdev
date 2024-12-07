using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class CalisanModel
    {
        [Key]
        public int Id { get; set; }

        public string KullaniciId { get; set; }
        public KullaniciModel Kullanici { get; set; }

        [ValidateNever]
        public ICollection<CalisanIslemModel> CalisanIslemleri { get; set; }
    }
}
