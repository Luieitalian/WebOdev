using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class KullaniciModel : IdentityUser
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }

        public CinsiyetEnum Cinsiyet { get; set; }
    }
}
