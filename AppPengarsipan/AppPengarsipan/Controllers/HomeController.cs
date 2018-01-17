using AppPengarsipan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPengarsipan.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize]
        public ActionResult LaporanSuratMasuk()
        {
            using (var db = new OcphDbContext())
            {
                var result = (from a in db.SuratMasuk.Select()
                              select new suratmasuk
                              {
                                  Asal = a.Asal,
                                  File = a.File,
                                  KodeSurat = a.KodeSurat,
                                  Lampiran = a.Lampiran,
                                  NomorSurat = a.NomorSurat,
                                  Perihal = a.Perihal,
                                  UserID = a.UserID,
                                  SuratMasukId = a.SuratMasukId,
                                  TanggalMasuk = a.TanggalMasuk,
                                  TanggalSurat = a.TanggalSurat,
                              }).ToList();

                return View(result);
            }
        }
        public ActionResult LaporanSuratKeluar()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.SuratKeluar.Select().ToList();

                return View(result);
            }
        }
        public ActionResult LaporanSuratDisposisi()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Disposisi.Select().ToList();

                return View(result);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}