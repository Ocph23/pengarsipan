using DAL.DContext;
using DAL.Repository;
using System;
using System.Data;
using System.Configuration;
using AppPengarsipan.Models;


namespace AppPengarsipan
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {

            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

       public IRepository<suratmasuk> SuratMasuk{ get { return new Repository<suratmasuk>(this); } }
        public IRepository<suratkeluar> SuratKeluar{ get { return new Repository<suratkeluar>(this); } }
        public IRepository<disposisi> Disposisi { get { return new Repository<disposisi>(this); } }
        public IRepository<petugas> Petugas { get { return new Repository<petugas>(this); } }

        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
