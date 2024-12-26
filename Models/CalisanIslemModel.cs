using System.Text.Json.Serialization;

namespace WebOdev.Models
{
    public class CalisanIslemModel
    {
        public int Yetkinlik { get; set; }
        public string? Not { get; set; }

        public string? CalisanId { get; set; }
        [JsonIgnore]
        public CalisanModel Calisan { get; set; }
        public int IslemId { get; set; }
        [JsonIgnore]
        public IslemModel Islem { get; set; }
    }
}
