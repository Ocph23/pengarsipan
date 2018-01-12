using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppPengarsipan.Api
{
    public class SummaryController : ApiController
    {
        // GET: api/Summary
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Summary/5
        public HttpResponseMessage Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var summary = new Models.SummaryInfo();
                summary.SuratMasuk = db.SuratMasuk.Select().Count();
                summary.SuratKeluar = db.SuratKeluar.Select().Count();
                summary.Disposisi = db.Disposisi.Select().Count();
                return Request.CreateResponse(HttpStatusCode.OK, summary);
            }
        }

        // POST: api/Summary
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Summary/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Summary/5
        public void Delete(int id)
        {
        }
    }
}
