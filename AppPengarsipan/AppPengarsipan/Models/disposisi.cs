using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppPengarsipan.Models 
{ 
     [TableName("disposisi")] 
     public class disposisi:BaseNotifyProperty  
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value; 
                     OnPropertyChange("Id");
                     }
          } 

          [DbColumn("Kode")] 
          public string Kode 
          { 
               get{return _kode;} 
               set{ 
                      _kode=value; 
                     OnPropertyChange("Kode");
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

          [DbColumn("Dari")] 
          public string Dari 
          { 
               get{return _dari;} 
               set{ 
                      _dari=value; 
                     OnPropertyChange("Dari");
                     }
          } 

          [DbColumn("TglPenyelesaian")] 
          public DateTime TglPenyelesaian 
          { 
               get{return _tglpenyelesaian;} 
               set{ 
                      _tglpenyelesaian=value; 
                     OnPropertyChange("TglPenyelesaian");
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

          [DbColumn("Isi")] 
          public string Isi 
          { 
               get{return _isi;} 
               set{ 
                      _isi=value; 
                     OnPropertyChange("Isi");
                     }
          } 

          [DbColumn("TanggalBuat")] 
          public DateTime TanggalBuat 
          { 
               get{return _tanggalbuat;} 
               set{ 
                      _tanggalbuat=value; 
                     OnPropertyChange("TanggalBuat");
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

          [DbColumn("SuratMasukId")] 
          public int SuratMasukId 
          { 
               get{return _suratmasukid;} 
               set{ 
                      _suratmasukid=value; 
                     OnPropertyChange("SuratMasukId");
                     }
          }

        public petugas Petugas { get; internal set; }

        private int  _id;
           private string  _kode;
           private string  _perihal;
           private string  _dari;
           private DateTime  _tglpenyelesaian;
           private string  _tujuan;
           private string  _isi;
           private DateTime  _tanggalbuat;
           private int  _petugasid;
           private int  _suratmasukid;
      }
}


