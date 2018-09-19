using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Model
{
    public class ModelSzakmai
    {
        public class Projekt_Bevont_struct
        {
            public int id { get; set; }
            public string megnevezes_projekt { get; set; }
            public string megnevezes_munka { get; set; }
            public int jeloltek_db { get; set; }
        }
    }
}
