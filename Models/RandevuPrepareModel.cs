using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebOdev.Models
{
    public class RandevuPrepareModel
    {
        public string? Id { get; set; }
        public string CalisanId { get; set; }
        public string MusterilerId { get; set; }
        public string IslemlerId { get; set; }
        public List<SelectListItem> Calisan { get; set; } = new();
        public List<SelectListItem> Musteriler { get; set; } = new();
        public List<SelectListItem> Islemler { get; set; } = new();
    }
}
