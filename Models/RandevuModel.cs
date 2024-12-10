﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebOdev.Models
{
    public class RandevuModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime? IstemTarihi { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public CalisanModel? Calisan { get; set; }
        public string? CalisanId { get; set; }
        public IslemModel? Islem { get; set; }
        public int? IslemId { get; set; }
        public MusteriModel? Musteri { get; set; }
        public string? MusteriId { get; set; }
    }
}
