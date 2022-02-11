using BizimMarket.Areas.Admin.Models;
using BizimMarket.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BizimMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunlerController : Controller
    {
        private readonly BizimMarketContext _db;
        private readonly IWebHostEnvironment _env;

        public UrunlerController(BizimMarketContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Urunler.Include(x => x.Kategori).ToList());
        }

        public IActionResult Yeni()
        {
            KategorileriYukle();
            return View();
        }
        //NOT: model binding nedir : Bir kompleks tip belirtildiğinde formdan gelen dataların action metotlardaki belirtilen türün proplarına aktarılma işlemine denir
        //Ancak bu model binding işleri esnasında prop ların üstündeki validation attiribute larda dikkate alınır (Required gibi)
        //Eğer bu hata mesajı uygun değilse modelstate e eklenir, model state e hata özelliği eklendiğinde isvalid false a döner, if e girmez
        //required modelstate deki kısımdaki hata mesajını required girdiğin an ezer
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(YeniUrunViewModel vm) //model binding
        {
            //tokatlama kısmı
            //#region Resim Geçerlilik
            //if (vm.Resim != null)
            //{
            //    if (!vm.Resim.ContentType.StartsWith("image/"))
            //        ModelState.AddModelError("Resim", "Geçersiz resim dosyası");
            //    else if (vm.Resim.Length > 1 * 1024 * 1024)
            //        ModelState.AddModelError("Resim", "Resim dosyası 1MB'den büyük olamaz");
            //}
            //#endregion
            //siliniyor burası

            if (ModelState.IsValid)
            {
                //harddiske kaydetme kısmı
                #region Resim Kaydetme
                //path kısmı resmin uzantısı için, jpg mi png mi vs, guid kısmı ise aldığımız resmin adını değiştirip benzersiz isimler vererek çakışmayı önlemek için
                //ResimYukle(); kısmı buradaydı
                #endregion

                Urun urun = new Urun()
                {
                    Ad = vm.Ad,
                    Fiyat = vm.Fiyat.Value,
                    KategoriId = vm.KategoriId.Value,
                    ResimYolu = ResimYukle(vm.Resim)
                };
                _db.Add(urun);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            KategorileriYukle();
            return View();
        }

        private string ResimYukle(IFormFile file)
        {
            string dosyaAdi = null;
            if (file != null)
            {
                dosyaAdi = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string kaydetmeYolu = Path.Combine(_env.WebRootPath, "img", "urunler", dosyaAdi);
                using (var fs = new FileStream(kaydetmeYolu, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
            }

            return dosyaAdi;
        }

        private void KategorileriYukle()
        {
            ViewBag.Kategoriler = _db.Kategoriler
                            .Select(x => new SelectListItem(
                            x.Ad, x.Id.ToString()))
                            .ToList();
        }

        public IActionResult Duzenle(int id)
        {
            var urun = _db.Urunler.Find(id);
            if (urun == null) return NotFound();

            var vm = new UrunDuzenleViewModel()
            {
                Id = urun.Id,
                Ad = urun.Ad,
                Fiyat = urun.Fiyat,
                KategoriId = urun.KategoriId,
                ResimYolu = urun.ResimYolu
            };

            KategorileriYukle();
            return View(vm);
        }

        //public IActionResult Duzenle(Urun urun, IFormFile yuklenenDosya)
        //{
        //attiribute (üste resim yolu için) giremeyeceğimizden bu yol elendi
        //}

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Duzenle(UrunDuzenleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //burada ürünü güncelle
                var urun = _db.Urunler.Find(vm.Id);
                urun.Ad = vm.Ad;
                urun.Fiyat = vm.Fiyat.Value;
                urun.KategoriId = vm.KategoriId.Value;
                if (vm.Resim != null)
                {
                    ResimSil(urun.ResimYolu);
                    //resmi yükle
                    urun.ResimYolu = ResimYukle(vm.Resim);
                    //yeni resim
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            KategorileriYukle();
            return View(vm);
        }

        public IActionResult Sil(int id)
        {
            var urun = _db.Urunler.Find(id);

            if (urun == null) return NotFound();
            ResimSil(urun.ResimYolu);
            _db.Urunler.Remove(urun);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void ResimSil(string dosyaAdi)
        {
            if (dosyaAdi == null) return;

            string silmeYolu = Path.Combine(_env.WebRootPath, "img", "urunler", dosyaAdi);

            //1.yol
            //if (System.IO.File.Exists(silmeYolu))
            //    System.IO.File.Delete(silmeYolu);

            //2.yol
            try
            {
                System.IO.File.Exists(silmeYolu);
            }
            catch (Exception)
            {
            }
        }
    }
}
