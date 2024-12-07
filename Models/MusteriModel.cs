﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebOdev.Models
{
    public class MusteriModel
    {
        [Key]
        public int Id { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DogumTarihi { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefon { get; set; }
        public CinsiyetEnum Cinsiyet { get; set; }
    }
}
