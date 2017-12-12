using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AppPengarsipan.Api
{
    public class FileController : ApiController
    {
        // GET: api/File
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/File/5
        public HttpResponseMessage Get(string file)
        {
            var path = HttpContext.Current.Server.MapPath("~/uploads/"+file);
          
            if (!File.Exists(path))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            Byte[] bytes = File.ReadAllBytes(path);
            //String file = Convert.ToBase64String(bytes);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition.FileName = file+".pdf";

            return response;

        }

        // POST: api/File
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            string uploadPath = HttpContext.Current.Server.MapPath("~/uploads");
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "This request is not properly formatted"));

            using (var db = new OcphDbContext())
            {
                var trans = db.Connection.BeginTransaction();
                try
                {
                    bool IsMasuk = false;
                    var provider = new MultipartFormDataStreamProvider(uploadPath);
                    await Request.Content.ReadAsMultipartAsync(provider);
                    var a = new Models.suratmasuk();
                    var b = new Models.suratkeluar();
                    FileInfo fi = null;

                    foreach (var file in provider.FileData)
                    {
                        fi = new FileInfo(file.LocalFileName);
                        a.File = fi.Name;
                        b.File = fi.Name;
                        // a.FileTipe = file.Headers.ContentType.MediaType;

                    }

                    foreach (HttpContent ctnt in provider.Contents)
                    {
                        var name = ctnt.Headers.ContentDisposition.Name;
                        var field = name.Substring(1, name.Length - 2);
                        if (field == "SuratMasukId")
                        {
                            a.SuratMasukId = Convert.ToInt32(await ctnt.ReadAsStringAsync());
                            b.SuratMasukId = a.SuratMasukId;
                        }
                        

                        if (field == "IsMasuk")
                        {
                            IsMasuk = Convert.ToBoolean(await ctnt.ReadAsStringAsync());
                        }
                    }

                    if(!string.IsNullOrEmpty(a.File) && IsMasuk)
                    {
                        if (!db.SuratMasuk.Update(O => new { O.File }, a, O => O.SuratMasukId == a.SuratMasukId))
                                throw new SystemException("Tidak Tersimpan ...");
                    }else
                    {
                        if (!db.SuratKeluar.Update(O => new { O.File },  b, O => O.SuratMasukId == a.SuratMasukId))
                            throw new SystemException("Tidak Tersimpan ...");
                    }
                    trans.Commit();
                    return Request.CreateResponse(HttpStatusCode.OK, a.File);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ex.Message);
                }
            }
        }

        // PUT: api/File/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/File/5
        public void Delete(int id)
        {
        }
    }
}
