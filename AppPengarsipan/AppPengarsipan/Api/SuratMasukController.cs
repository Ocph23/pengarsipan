﻿using AppPengarsipan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace AppPengarsipan.Api
{
    public class SuratMasukController : ApiController
    {
        // GET: api/SuratMasuk
        public IEnumerable<suratmasuk> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.SuratMasuk.Select()
                             join c in db.Petugas.Select() on a.PetugasId equals c.PetugasId
                           
                             select new suratmasuk
                             {
                                 Asal = a.Asal,
                                 File = a.File,
                                 KodeSurat = a.KodeSurat,
                                 Lampiran = a.Lampiran,
                                 NomorSurat = a.NomorSurat,
                                 Perihal = a.Perihal,
                                 PetugasId = a.PetugasId,
                                 SuratMasukId = a.SuratMasukId,
                                 TanggalMasuk = a.TanggalMasuk,
                                 TanggalSurat = a.TanggalSurat,
                                 Petugas=c
                             };

                return result.ToList();
            }
        }

        // GET: api/SuratMasuk/5
        public suratmasuk Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.SuratMasuk.Where(O=>O.SuratMasukId==id)
                             join c in db.Petugas.Select() on a.PetugasId equals c.PetugasId
                             join b in db.Disposisi.Select().DefaultIfEmpty() on a.SuratMasukId equals b.SuratMasukId
                             select new suratmasuk
                             {
                                 Asal = a.Asal,
                                 File = a.File,
                                 KodeSurat = a.KodeSurat,
                                 Lampiran = a.Lampiran,
                                 NomorSurat = a.NomorSurat,
                                 Perihal = a.Perihal,
                                 PetugasId = a.PetugasId,
                                 SuratMasukId = a.SuratMasukId,
                                 TanggalMasuk = a.TanggalMasuk,
                                 TanggalSurat = a.TanggalSurat,
                                 Disposisi = b,
                                 Petugas = c
                             };

                return result.FirstOrDefault();
            }
        }

        // POST: api/SuratMasuk
        public HttpResponseMessage Post([FromBody]suratmasuk value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if(ModelState.IsValid)
                    {
                       var uId= User.Identity.GetUserId();
                        value.PetugasId = db.Petugas.Where(O => O.UserId == uId).FirstOrDefault().PetugasId;
                        value.SuratMasukId = db.SuratMasuk.InsertAndGetLastID(value);
                        if (value.SuratMasukId > 0)
                            return Request.CreateResponse(HttpStatusCode.OK, value);
                        else
                            throw new SystemException("Data Tidak Tersimpan");
                    }else
                    {
                        throw new SystemException("Model Error");
                    }
                }
                catch (Exception ex)
                {
                    if(ex.Message== "Model Error")
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);
                    }else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                    }
                    
                }
            }



        }

        // PUT: api/SuratMasuk/5
        public HttpResponseMessage Put(int id, [FromBody]suratmasuk value)
        {

            using (var db = new OcphDbContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        value.SuratMasukId = db.SuratMasuk.InsertAndGetLastID(value);
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
                    if(result!=null)
                    {
                        if (result.Disposisi!=null && db.Disposisi.Delete(O => O.SuratMasukId == result.Disposisi.Id))
                        {
                            if(db.SuratMasuk.Delete(O=>O.SuratMasukId==id))
                            {
                                trans.Commit();
                                return Request.CreateResponse(HttpStatusCode.OK, "Data Tersimpan");
                            }else
                            {
                                throw new SystemException("Data Tidak Tersimpan");
                            }
                        }else
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