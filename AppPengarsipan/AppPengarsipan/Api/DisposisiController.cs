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
    public class DisposisiController : ApiController
    {
        // GET: api/Disposisi
        public IEnumerable<disposisi> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Disposisi.Select()
                             select new disposisi
                             {
                                 Tujuan = a.Tujuan,
                                 Perihal = a.Perihal,
                                 UserId = a.UserId,
                                 SuratMasukId = a.SuratMasukId,
                             };
                return result.ToList();
            }
        }

        // GET: api/SuratMasuk/5
        public HttpResponseMessage Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = from a in db.Disposisi.Where(O => O.SuratMasukId == id)
                                 select new disposisi
                                 {
                                     Dari = a.Dari,
                                     Id = a.Id,
                                     Isi = a.Isi,
                                     Kode = a.Kode,
                                     Perihal = a.Perihal,
                                     UserId = a.UserId,
                                     SuratMasukId = a.SuratMasukId,
                                     TanggalBuat = a.TanggalBuat,
                                     TglPenyelesaian = a.TanggalBuat,
                                     Tujuan = a.Tujuan,
                                 };

                    return Request.CreateResponse(HttpStatusCode.OK, result.FirstOrDefault());
                }
                catch (Exception ex)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
                }
               
            }
        }

        // POST: api/SuratMasuk
        public HttpResponseMessage Post([FromBody]disposisi value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var uId = User.Identity.GetUserId();
                        value.UserId = uId;
                        value.Id= db.Disposisi.InsertAndGetLastID(value);
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
        public HttpResponseMessage Put(int id, [FromBody]disposisi value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var isUpdated = db.Disposisi.Update(O => new { O.Kode, O.Dari, O.Isi, O.Perihal, O.UserId, O.TglPenyelesaian, O.Tujuan }, 
                            value,O => O.Id == value.Id);
                        if (isUpdated)
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
                        if (db.Disposisi.Delete(O => O.Id == id))
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
