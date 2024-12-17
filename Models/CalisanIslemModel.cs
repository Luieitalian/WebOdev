namespace WebOdev.Models
{
    public class CalisanIslemModel
    {
        public int Yetkinlik { get; set; }
        public string? Not { get; set; }

        public string? CalisanId { get; set; }
        public CalisanModel Calisan { get; set; }
        public int IslemId { get; set; }
        public IslemModel Islem { get; set; }
    }
}
