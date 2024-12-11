using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class RandevuModelDto
    {
        public int? Id { get; set; }
        public DateTime? IstemTarihi { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string? CalisanAdi { get; set; }
        public string? IslemAdi { get; set; }
        public string? MusteriAdi { get; set; }
        public RandevuPrepareModel? Prepare { get; set; } = new();
    }
}
