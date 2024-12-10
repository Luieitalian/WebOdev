using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class RandevuModelDto
    {
        public int? Id { get; set; }
        public DateTime? IstemTarihi { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string? CalisanId { get; set; }
        public int? IslemId { get; set; }
        public string? MusteriId { get; set; }
        public RandevuPrepareModel? Prepare { get; set; } = new();
    }
}
