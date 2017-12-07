using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppPengarsipan.Models 
{ 
     [TableName("suratmasuk")] 
     public class suratmasuk:BaseNotifyProperty  
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

          

          [DbColumn("KodeSurat")] 
          public string KodeSurat 
          { 
               get{return _kodesurat;} 
               set{ 
                      _kodesurat=value; 
                     OnPropertyChange("KodeSurat");
                     }
          } 

          [DbColumn("Asal")] 
          public string Asal 
          { 
               get{return _asal;} 
               set{ 
                      _asal=value; 
                     OnPropertyChange("Asal");
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

            [DbColumn("TanggalMasuk")]
        public DateTime TanggalMasuk
        {
            get
            {
                if (_tanggalmasuk == new DateTime())
                    _tanggalmasuk = DateTime.Now;
                return _tanggalmasuk;
            }
            set
            {
                _tanggalmasuk = value;
                OnPropertyChange("TanggalMasuk");
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
        public disposisi Disposisi { get; internal set; }

        private int  _suratmasukid;
           private DateTime  _tanggalmasuk;
           private string  _kodesurat;
           private string  _asal;
           private string  _nomorsurat;
           private DateTime  _tanggalsurat;
           private string  _perihal;
           private int  _lampiran;
           private string  _file;
           private int  _petugasid;
      }
}


