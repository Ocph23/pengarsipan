    using AppPengarsipan.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppPengarsipan.Api
{
    public class SuratKeluarController : ApiController
    {
        public IEnumerable<suratkeluar> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.SuratKeluar.Select()
                             select new suratkeluar
                             {
                                 Tujuan = a.Tujuan,
                                 File = a.File,
                                 KodeSurat = a.KodeSurat,
                                 Lampiran = a.Lampiran,
                                 NomorSurat = a.NomorSurat,
                                 Perihal = a.Perihal,
                                 UserId = a.UserId,
                                 SuratMasukId = a.SuratMasukId,
                                 TanggalKeluar = a.TanggalKeluar,
                                 TanggalSurat = a.TanggalSurat
                             };
                return result.ToList();
            }
        }

        // GET: api/SuratMasuk/5
        public suratkeluar Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.SuratKeluar.Where(O => O.SuratMasukId == id)
                             select new suratkeluar
                             {
                                 Tujuan = a.Tujuan,
                                 File = a.File,
                                 KodeSurat = a.KodeSurat,
                                 Lampiran = a.Lampiran,
                                 NomorSurat = a.NomorSurat,
                                 Perihal = a.Perihal,
                                 UserId = a.UserId,
                                 SuratMasukId = a.SuratMasukId,
                                 TanggalKeluar = a.TanggalKeluar,
                                 TanggalSurat = a.TanggalSurat,
                             };

                return result.FirstOrDefault();
            }
        }

        // POST: api/SuratMasuk
        public HttpResponseMessage Post([FromBody]suratkeluar value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var uId = User.Identity.GetUserId();
                        value.UserId = uId;
                        value.SuratMasukId = db.SuratKeluar.InsertAndGetLastID(value);
                        if (value.SuratMasukId > 0)
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }
                    else
                    {
                        throw new SystemException("Model Error");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Model Error")
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }

                }
            }



        }

        // PUT: api/SuratMasuk/5
        public HttpResponseMessage Put(int id, [FromBody]suratkeluar value)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var updated = db.SuratKeluar.Update(O => new {O.KodeSurat,O.Lampiran,O.NomorSurat,O.Perihal,O.TanggalKeluar,O.TanggalSurat,O.Tujuan,O.UserId }, value, O => O.SuratMasukId == value.SuratMasukId);
                        if (updated)
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }
                    else
                    {
                        throw new SystemException("Model Error");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Model Error")
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }

                }
            }
        }

        // DELETE: api/SuratMasuk/5
        public HttpResponseMessage Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    var result = this.Get(id);
                    if (result != null)
                    {
                        if (db.SuratMasuk.Delete(O => O.SuratMasukId == id))
                        {
                            trans.Commit();
                            return Request.CreateResponse(HttpStatusCode.OK, "Data Tersimpan");
                        }
                        else
                        {
                            throw new SystemException("Data Tidak Tersimpan");
                        }
                    }
                    else
                    {
                        throw new SystemException("Data Tidak Ditemukan");
                    }

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }

        }
    }
}
