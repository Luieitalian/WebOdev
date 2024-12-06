namespace WebOdev.Models
{
    public class CalisanIslemModel
    {
        public int CalisanID { get; set; }
        public int IslemID { get; set; }
        public int Yetkinlik { get; set; }
        public string Not { get; set; }

        public CalisanModel Calisan { get; set; }
        public IslemModel Islem { get; set; }
    }
}
