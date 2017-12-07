using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppPengarsipan.Models 
{ 
     [TableName("suratkeluar")] 
     public class suratkeluar:BaseNotifyProperty  
   {
          [PrimaryKey("SuratMasukId")] 
          [DbColumn("SuratMasukId")] 
          public int SuratMasukId 
          { 
               get{return _suratmasukid;} 
               set{ 
                      _suratmasukid=value; 
                     OnPropertyChange("SuratMasukId");
                     }
          } 

          [DbColumn("TanggalKeluar")] 
          public DateTime TanggalKeluar 
          { 
               get{return _tanggalkeluar;} 
               set{ 
                      _tanggalkeluar=value; 
                     OnPropertyChange("TanggalKeluar");
                     }
          } 

          [DbColumn("KodeSurat")] 
          public string KodeSurat 
          { 
               get{return _kodesurat;} 
               set{ 
                      _kodesurat=value; 
                     OnPropertyChange("KodeSurat");
                     }
          } 

          [DbColumn("Tujuan")] 
          public string Tujuan 
          { 
               get{return _tujuan;} 
               set{ 
                      _tujuan=value; 
                     OnPropertyChange("Tujuan");
                     }
          } 

          [DbColumn("NomorSurat")] 
          public string NomorSurat 
          { 
               get{return _nomorsurat;} 
               set{ 
                      _nomorsurat=value; 
                     OnPropertyChange("NomorSurat");
                     }
          } 

          [DbColumn("TanggalSurat")] 
          public DateTime TanggalSurat 
          { 
               get{return _tanggalsurat;} 
               set{ 
                      _tanggalsurat=value; 
                     OnPropertyChange("TanggalSurat");
                     }
          } 

          [DbColumn("Perihal")] 
          public string Perihal 
          { 
               get{return _perihal;} 
               set{ 
                      _perihal=value; 
                     OnPropertyChange("Perihal");
                     }
          } 

          [DbColumn("Lampiran")] 
          public int Lampiran 
          { 
               get{return _lampiran;} 
               set{ 
                      _lampiran=value; 
                     OnPropertyChange("Lampiran");
                     }
          } 

          [DbColumn("File")] 
          public string File 
          { 
               get{return _file;} 
               set{ 
                      _file=value; 
                     OnPropertyChange("File");
                     }
          } 

          [DbColumn("PetugasId")] 
          public int PetugasId 
          { 
               get{return _petugasid;} 
               set{ 
                      _petugasid=value; 
                     OnPropertyChange("PetugasId");
                     }
          }

        public petugas Petugas { get; internal set; }

        private int  _suratmasukid;
           private DateTime  _tanggalkeluar;
           private string  _kodesurat;
           private string  _tujuan;
           private string  _nomorsurat;
           private DateTime  _tanggalsurat;
           private string  _perihal;
           private int  _lampiran;
           private string  _file;
           private int  _petugasid;
      }
}


