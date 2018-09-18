using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Model
{
    public class Projekt_Search_Memory
    {
        public int statusz { get; set; }

    }

    public class SmallProjectListItems
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
        public int jeloltek_db { get; set; }
    }

    public class SubProjekt
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
    }

    public class ComboBox_Seged_Struct
    {
        public int id { get; set; }
    }

    public class ProjectListItems
    {
        public int id { get; set; }
        public string megnevezes_projekt { get; set; }
        public string megnevezes_munka { get; set; }
        public int jeloltek_db { get; set; }
        public int interjuk_db { get; set; }
        public int statusz { get; set; }
        public string fel_datum { get; set; }
        public int Completion { get; set; }
    }

    public class ProjectExtendedListItems
    {
        public int id { get; set; }
        public int hr_id { get; set; }
        public string megnevezes_projekt { get; set; }
        public string megnevezes_vegzettseg { get; set; }
        public string megnevezes_nyelv { get; set; }
        public string megnevezes_munka { get; set; }
        public string megnevezes_pc { get; set; }
        public string megnevezes_hr { get; set; }
        public string fel_datum { get; set; }
        public string le_datum { get; set; }
        public int pc { get; set; }
        public int vegzettseg { get; set; }
        public int tapasztalat_ev { get; set; }
        public string statusz { get; set; }
        public int publikalt { get; set; }
        public int nyelvtudas { get; set; }
        public int munkakor { get; set; }
        public int szuldatum { get; set; }
        public int ber { get; set; }
        public int jeloltek_db { get; set; }
        public int kepesseg1 { get; set; }
        public int kepesseg2 { get; set; }
        public int kepesseg3 { get; set; }
        public int kepesseg4 { get; set; }
        public int kepesseg5 { get; set; }
        public string feladatok { get; set; }
        public string elvarasok { get; set; }
        public string kinalunk { get; set; }
        public string elonyok { get; set; }
    }

    public class ProjectInsertListItems
    {
        public int id { get; set; }
        public int hr_id { get; set; }
        public string megnevezes_projekt { get; set; }
        public int pc { get; set; }
        public int vegzettseg { get; set; }
        public int tapasztalat_ev { get; set; }
        public int statusz { get; set; }
        public string fel_datum { get; set; }
        public string le_datum { get; set; }
        public int nyelvtudas { get; set; }
        public int munkakor { get; set; }
        public int szuldatum { get; set; }
        public int ber { get; set; }
        public int kepesseg1 { get; set; }
        public int kepesseg2 { get; set; }
        public int kepesseg3 { get; set; }
        public int kepesseg4 { get; set; }
        public int kepesseg5 { get; set; }
        public string feladatok { get; set; }
        public string elvarasok { get; set; }
        public string kinalunk { get; set; }
    }

    public class kompetenciak
    {
        public int id { get; set; }
        public string kompetencia_megnevezes { get; set; }
    }

    public class koltsegek
    {
        public int id { get; set; }
        public int osszeg { get; set; }
        public string koltseg_megnevezes { get; set; }
    }
}
