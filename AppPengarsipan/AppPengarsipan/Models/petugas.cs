using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
 
 namespace AppPengarsipan.Models 
{ 
     [TableName("petugas")] 
     public class petugas:BaseNotifyProperty  
   {
          [PrimaryKey("PetugasId")] 
          [DbColumn("PetugasId")] 
          public int PetugasId 
          { 
               get{return _petugasid;} 
               set{ 
                      _petugasid=value; 
                     OnPropertyChange("PetugasId");
                     }
          } 

          [DbColumn("Nama")] 
          public string Nama 
          { 
               get{return _nama;} 
               set{ 
                      _nama=value; 
                     OnPropertyChange("Nama");
                     }
          } 

          [DbColumn("Email")] 
          public string Email 
          { 
               get{return _email;} 
               set{ 
                      _email=value; 
                     OnPropertyChange("Email");
                     }
          } 

          [DbColumn("NoInduk")] 
          public string NoInduk 
          { 
               get{return _noinduk;} 
               set{ 
                      _noinduk=value; 
                     OnPropertyChange("NoInduk");
                     }
          } 

          [DbColumn("Jabatan")] 
          public string Jabatan 
          { 
               get{return _jabatan;} 
               set{ 
                      _jabatan=value; 
                     OnPropertyChange("Jabatan");
                     }
          }

        [DbColumn("UserId")]
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChange("UserId");
            }
        }

        private int  _petugasid;
           private string  _nama;
           private string  _email;
           private string  _noinduk;
           private string  _jabatan;
        private string _userId;
    }
}


