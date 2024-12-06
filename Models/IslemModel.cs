﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class IslemModel
    {
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public TimeSpan Uzunluk { get; set; }
        public int Ucret { get; set; }
        public string Cinsiyet { get; set; }

        [ValidateNever]
        public ICollection<CalisanIslemModel> CalisanIslemleri { get; set; }
    }

    
}