using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class KullaniciEkleViewModel
    {
        public string Isim { get; set; }
        public string Soyisim { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }

        public CinsiyetEnum Cinsiyet { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Doğru bir email adresi giriniz!")]
        public string Email { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Lütfen geçerli bir şifre giriniz!")]
        public string? Sifre { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz!")]
        public string Telefon { get; set; }
        public string? Id { get; set; }
    }
}
