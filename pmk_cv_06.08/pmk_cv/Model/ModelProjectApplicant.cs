using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Model
{
    public class projekt_jelolt_kapcs
    {
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public int hr_id { get; set; }
        public string datum { get; set; }
    }

    public class interju_struct
    {
        public int id { get; set; }
        public string projekt_megnevezes { get; set; }
        public string jelolt_megnevezes { get; set; }
        public string jelolt_email { get; set; }
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public int hr_id { get; set; }
        public string felvitel_datum { get; set; }
        public string interju_datum { get; set; }
        public string interju_cim { get; set; }
        public string interju_leiras { get; set; }
        public string helyszin { get; set; }
        public string idopont { get; set; }
    }

    //public class tamogatas_struct
    //{
    //    public int id { get; set; }
    //    public int hr_id { get; set; }
    //    public int projekt_id { get; set; }
    //    public int ertekeles { get; set; }
    //    public int tamogatja { get; set; }
    //}

    public class kompetencia_jelolt_kapcs_struct
    {
        public int id { get; set; }
        public int interju_id { get; set; }
        public int projekt_id { get; set; }
        public int jelolt_id { get; set; }
        public int hr_id { get; set; }
        public int k1_id { get; set; }
        public int k1_val { get; set; }
        public int k2_id { get; set; }
        public int k2_val { get; set; }
        public int k3_id { get; set; }
        public int k3_val { get; set; }
        public int k4_id { get; set; }
        public int k4_val { get; set; }
        public int k5_id { get; set; }
        public int k5_val { get; set; }
        public int tamogatom { get; set; }
    }
    
    public class kompetencia_summary_struct
    {
        
        public int k1_val { get; set; }
        public int k2_val { get; set; }
        public int k3_val { get; set; }
        public int k4_val { get; set; }
        public int k5_val { get; set; }
        public int tamogatom { get; set; }

    }

    public class kompetencia_tamogatas
    {
        public int tamogatom { get; set; }
    }
}
