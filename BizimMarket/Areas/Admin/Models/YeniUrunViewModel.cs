using BizimMarket.Attiributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket.Areas.Admin.Models
{
    //konu anlatım özeti
    //https://stackoverflow.com/questions/35379309/how-to-upload-files-in-asp-net-core
    public class YeniUrunViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunlu.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Fiyat alanı zorunlu.")]
        public decimal? Fiyat { get; set; }

        [Required(ErrorMessage = "Kategori alanı zorunlu.")]
        public int? KategoriId { get; set; }

        //<input type="file" name="Resim" /> şeklinde koymalıyız
        [GecerliResim(MaksimumDosyaBoyutuMb = 2)]
        public IFormFile Resim { get; set; }
    }
}
