﻿using AppPengarsipan.Models;
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
                             join c in db.Petugas.Select() on a.PetugasId equals c.PetugasId
                             select new suratkeluar
                             {
                                 Tujuan = a.Tujuan,
                                 File = a.File,
                                 KodeSurat = a.KodeSurat,
                                 Lampiran = a.Lampiran,
                                 NomorSurat = a.NomorSurat,
                                 Perihal = a.Perihal,
                                 PetugasId = a.PetugasId,
                                 SuratMasukId = a.SuratMasukId,
                                 TanggalKeluar = a.TanggalKeluar,
                                 TanggalSurat = a.TanggalSurat,
                                 Petugas = c
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
                             join c in db.Petugas.Select() on a.PetugasId equals c.PetugasId
                             select new suratkeluar
                             {
                                 Tujuan = a.Tujuan,
                                 File = a.File,
                                 KodeSurat = a.KodeSurat,
                                 Lampiran = a.Lampiran,
                                 NomorSurat = a.NomorSurat,
                                 Perihal = a.Perihal,
                                 PetugasId = a.PetugasId,
                                 SuratMasukId = a.SuratMasukId,
                                 TanggalKeluar = a.TanggalKeluar,
                                 TanggalSurat = a.TanggalSurat,
                                 Petugas = c
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