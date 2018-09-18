using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Model
{

    public class JeloltListItems
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int szuldatum { get; set; }
        public string email { get; set; }
        public int interjuk_db { get; set; }
        public int allapota { get; set; }
        public string kolcsonzott { get; set; }
        public string allapot_megnevezes { get; set; }
        public string reg_datum { get; set; }
        public bool Checked { get; set; }
    }

    public class JeloltListBox
    {
        public int id { get; set; }
        public string nev { get; set; }
        public int interjuk_db { get; set; }
    }

    public class SubJelolt
    {
        public int id { get; set; }
        public string nev { get; set; }
    }

    public class JeloltExtendedList
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string email { get; set; }
        public string telefon { get; set; }
        public string lakhely { get; set; }
        public string ertesult { get; set; }
        public int id_ertesult { get; set; }
        public int szuldatum { get; set; }
        public string neme { get; set; }
        public int id_neme { get; set; }
        public int tapasztalat_ev { get; set; }
        public string munkakor { get; set; }
        public string munkakor2 { get; set; }
        public string munkakor3 { get; set; }
        public int id_munkakor { get; set; }
        public int id_munkakor2 { get; set; }
        public int id_munkakor3 { get; set; }
        public string vegz_terulet { get; set; }
        public int id_vegz_terulet { get; set; }
        public string nyelvtudas { get; set; }
        public string nyelvtudas2 { get; set; }
        public int id_nyelvtudas { get; set; }
        public int id_nyelvtudas2 { get; set; }
        public string reg_date { get; set; }
        public string megjegyzes { get; set; }
        public string folderUrl { get; set; }
    }

    public class JeloltSearchItems
    {
        public string nev { get; set; }
        public string lakhely { get; set; }
        public string email { get; set; }
        public int szuldatum { get; set; }
        public int tapasztalat { get; set; }
        public string regdate { get; set; }
        public int interju { get; set; }
        public int neme { get; set; }
        public string munkakor { get; set; }
        public string vegztettseg { get; set; }
    }

    public class nyelv_struct
    {
        public int id { get; set; }
        public string nyelv { get; set; }
    }

    public class ertesulesek
    {
        public int id { get; set; }
        public string ertesules_megnevezes { get; set; }
    }

    public class neme_struct
    {
        public int id { get; set; }
        public string nem { get; set; }
    }

    public class munkakor_struct
    {
        public int id { get; set; }
        public string munkakor { get; set; }
    }

    public class statusz_struct
    {
        public int id { get; set; }
        public string allapot { get; set; }
    }

    public class pc_struct
    {
        public int id { get; set; }
        public string megnevezes_pc { get; set; }
    }

    public class vegzettseg_struct
    {
        public int id { get; set; }
        public string megnevezes_vegzettseg { get; set; }
    }

    public class ertesitendok_struct
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class hr_struct
    {
        public int id { get; set; }
        public string name { get; set; }
        public int kategoria { get; set; }
        public int jogosultsag { get; set; }
        public int validitas { get; set; }
    }
    
    public class megjegyzes_struct
    {
        public int id { get; set; }
        public int jelolt_id { get; set; }
        public int projekt_id { get; set; }
        public int hr_id { get; set; }
        public string hr_nev { get; set; }
        public string megjegyzes { get; set; }
        public string datum { get; set; }
        public int ertekeles { get; set; }
    }

    public class ftp_feltoltes
    {
        public bool allapot { get; set; }
        public string kiterjesztes { get; set; }
    }

    public class csatolmany_struct
    {
        public int kapcs_id { get; set; }
        public int tipus { get; set; }
        public string fajlnev { get; set; }
        public string kiterjesztes { get; set; }
        public string kep { get; set; }
    }

    public class Jelolt_File_Struct
    {
        public string fajlnev { get; set; }
        public string path { get; set; }
    }

    public class activity_struct
    {
        public int id { get; set; }
        public int hr_id { get; set; }
        public string nev { get; set; }
        public string tipus { get; set; }
        public string leiras { get; set; }
    }
}
