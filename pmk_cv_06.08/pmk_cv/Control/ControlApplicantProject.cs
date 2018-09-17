﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRCloud.Control;
using HRCloud.Model;

namespace HRCloud.Control
{
    class ControlApplicantProject
    {
        ControlApplicant acontrol = new ControlApplicant();
        ControlProject pcontrol = new ControlProject();
        Session sess = new Session();
        Model.MySql dbE = new Model.MySql();
        private static int InterjuIDs;
        public int InterjuID { get { return InterjuIDs; } set { InterjuIDs = value; } }
        private static int TelefonSzurts;
        public int TelefonSzurt { get { return TelefonSzurts; } set { TelefonSzurts = value; } }

        public void AddInterPlusOne()
        {
            string query = "UPDATE projekt_jelolt_kapcs SET allapot = allapot + 1 WHERE projekt_id = "+pcontrol.ProjektID+ " AND jelolt_id = "+acontrol.ApplicantID+"";
            dbE.update(query);
        }
        public void progress_delete()
        {
            string query = "UPDATE projekt_jelolt_kapcs SET allapot = allapot - 1 WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
            dbE.update(query);
        }
        public void Telefon_Szures_Elfogad(int ismerte,int muszakok,string utazas)
        {
            string query1 = "UPDATE projekt_jelolt_kapcs SET allapota = 1 WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
            string query2 = "UPDATE jeloltek SET pmk_ismerte = "+ismerte+"  WHERE id = " + acontrol.ApplicantID + "";
            string query3 = "INSERT INTO jelolt_statisztika (id, jelolt_id, utazas, muszakok) VALUES(null, "+acontrol.ApplicantID+", '"+utazas+"', "+muszakok+")";
            dbE.update(query1);
            dbE.update(query2);
            dbE.update(query3);
        }
        public void Telefon_Szures_Elutasit()
        {
            string query1 = "UPDATE projekt_jelolt_kapcs SET telefonos_szures = 0  WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
            string query2 = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
            string query3 = "DELETE FROM megjegyzesek WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
            string query4 = "DELETE FROM interjuk_kapcs WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";

            dbE.update(query1);
            dbE.update(query2);
            dbE.update(query3);
            dbE.update(query4);
        }
        public List<interju_struct> Interju_DataSource()
        {
            string query = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE jelolt_id = " + acontrol.ApplicantID+"" +
                " AND projekt_id="+pcontrol.ProjektID+"" +
                " ORDER BY felvitel_datum";
            return dbE.Interju_MySql_listQuery(query);
        }
        public List<interju_struct> Interju_DataSource_ByID()
        {
            string query = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE interjuk_kapcs.id = " + InterjuID + "" +
                " ORDER BY felvitel_datum";
            return dbE.Interju_MySql_listQuery(query);
        }

        public void addInterview(string interju_datum, string cim, string leiras, string helyszin, string idopont)
        {

                DateTime dateTime = DateTime.Now;
                string query = "INSERT INTO `interjuk_kapcs` (`projekt_id`, `jelolt_id`, `hr_id`, `felvitel_datum`, `interju_datum`, `interju_cim`, `interju_leiras`, `helyszin`,  `idopont`) VALUES (" + pcontrol.ProjektID + ", " + acontrol.ApplicantID + ", " + sess.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "', '" + interju_datum + "', '" + cim + "', '" + leiras + "', '" + helyszin + "', '" + idopont + "');";
                dbE.update(query);
       

        }
        public void interju_delete(int id)
        {
            string query1 = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.id="+id+" AND hr_id="+sess.UserData[0].id+"";
            dbE.update(query1);
            string query2 = "DELETE FROM interju_resztvevo_kapcs WHERE interju_id=" + id + " ";
            dbE.update(query2);
        }
        public List<kompetenciak> kompetencia_DataSource()
        {
            string query = "SELECT * FROM kompetenciak";
            return dbE.Kompetenciak_MySql_listQuery(query);
        }
        public List<kompetencia_summary_struct> kompetencia_jelolt_kapcs_DataSource()
        {
            string query = "SELECT coalesce(AVG(k1_val),0) as k1_val,coalesce(AVG(k2_val),0) as k2_val,coalesce(AVG(k3_val),0) as k3_val,coalesce(AVG(k4_val),0) as k4_val,coalesce(AVG(k5_val),0) as k5_val, tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + acontrol.ApplicantID+" AND projekt_id = "+pcontrol.ProjektID+"";
            return dbE.Kompetencia_summary_MySql_listQuery(query);
        }
        
        public List<kompetencia_tamogatas> kompetencia_tamogatas_DataSource()
        {
            string query = "SELECT tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + acontrol.ApplicantID + " AND projekt_id = " + pcontrol.ProjektID + "";
            return dbE.Kompetencia_tamogatas_MySql_listQuery(query);
        }

        public void Kopmetencia_ertekeles_INSERT(List<int> list)
        {
            string query = "INSERT INTO `kompetencia_jelolt_kapcs` (`id`, `interju_id`, `projekt_id`, `jelolt_id`, `hr_id`, `k1_id`, `k1_val`, `k2_id`, `k2_val`, `k3_id`, `k3_val`, `k4_id`, `k4_val`, `k5_id`, `k5_val`, tamogatom) VALUES (null, " + InterjuID + ", " + pcontrol.ProjektID + ", " + acontrol.ApplicantID + ", " + sess.UserData[0].id + ", "+list[0]+ ", " + list[1] + ", " + list[2] + ", " + list[3] + ", " + list[4] + ", " + list[5] + ", " + list[6] + ", " + list[7] + ", " + list[8] + ", " + list[9] + ", " + list[10] + ");";
            dbE.update(query);
        }
        public bool Kompetencia_valider()
        {
            string query = "SELECT * FROM kompetencia_jelolt_kapcs WHERE interju_id = "+ InterjuID + " AND hr_id = "+sess.UserData[0].id+"";
            return dbE.bind(query);
        }
        public List<ertesitendok_struct> bevon_ertesitendok_DataSource()
        {
            string query = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON ertesitendok_id = users.id WHERE projekt_id = "+pcontrol.ProjektID+"";
            return dbE.Ertesitendok_MySql_listQuery(query);
        }
        public List<ertesitendok_struct> interjuhoz_adott_ertesitendok_DataSource()
        {
            string query = "SELECT users.id, name, email FROM interju_resztvevo_kapcs left JOIN users ON user_id = users.id WHERE interju_id = " + InterjuID + " GROUP BY users.id";
            return dbE.Ertesitendok_MySql_listQuery(query);
        }
        public void Write_User_To_Inerju(int id)
        {
            string query = "INSERT INTO `interju_resztvevo_kapcs` (`id`, `interju_id`, `user_id`) VALUES (NULL, "+InterjuID+", "+id+");";
            dbE.update(query);
        }
        public void Delete_User_To_Inerju(int id)
        {
            string query = "DELETE FROM `interju_resztvevo_kapcs` WHERE user_id = " + id + "";
            dbE.update(query);
        }

    }
}