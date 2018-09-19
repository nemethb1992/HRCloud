using System;
using System.Collections.Generic;
using HRCloud.Model;
using HRCloud.Source;

namespace HRCloud.Control
{
    class ControlApplicantProject
    {
        private static int InterjuIDs;
        public int InterjuID { get { return InterjuIDs; } set { InterjuIDs = value; } }

        private static int TelefonSzurts;
        public int TelefonSzurt { get { return TelefonSzurts; } set { TelefonSzurts = value; } }

        ControlApplicant aControl = new ControlApplicant();
        ControlProject pControl = new ControlProject();
        Session session = new Session();
        Model.MySql mySql = new Model.MySql();

        public void telephoneFilterInsert(int ismerte,int muszakok,string utazas) //javított
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = 1 WHERE projekt_id = " + pControl.ProjektID + " AND jelolt_id = " + aControl.ApplicantID + "";
            mySql.update(command);
            command = "UPDATE jeloltek SET pmk_ismerte = "+ismerte+"  WHERE id = " + aControl.ApplicantID + "";
            mySql.update(command);
            command = "INSERT INTO jelolt_statisztika (id, jelolt_id, utazas, muszakok) VALUES(null, "+aControl.ApplicantID+", '"+utazas+"', "+muszakok+")";
            mySql.update(command);
            mySql.close();
        }
        
        public List<interju_struct> Data_Interview() //javított
        {
            string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE jelolt_id = " + aControl.ApplicantID+"" +
                " AND projekt_id="+pControl.ProjektID+"" +
                " ORDER BY felvitel_datum";
            List<interju_struct> list = mySql.Interju_MySql_listQuery(command);
            mySql.close();
            return list;
        }

        public List<interju_struct> Data_InterviewById() //javított
        {
            string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE interjuk_kapcs.id = " + InterjuID + "" +
                " ORDER BY felvitel_datum";
            List<interju_struct> list = mySql.Interju_MySql_listQuery(command);
            mySql.close();
            return list;
        }

        public void addInterview(string interju_datum, string cim, string leiras, string helyszin, string idopont) // javítva
        {
            DateTime dateTime = DateTime.Now;
            string command = "INSERT INTO `interjuk_kapcs` (`projekt_id`, `jelolt_id`, `hr_id`, `felvitel_datum`, `interju_datum`, `interju_cim`, `interju_leiras`, `helyszin`,  `idopont`) VALUES (" + pControl.ProjektID + ", " + aControl.ApplicantID + ", " + session.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "', '" + interju_datum + "', '" + cim + "', '" + leiras + "', '" + helyszin + "', '" + idopont + "');";
            mySql.update(command);
            mySql.close();
        }

        public void interviewDelete(int id) // javítva
        {
            mySql.update("DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.id=" + id + " AND hr_id=" + session.UserData[0].id + "");
            mySql.update("DELETE FROM interju_resztvevo_kapcs WHERE interju_id=" + id + " ");
            mySql.close();
        }

        public List<kompetenciak> Data_Kompetencia() // javítva használja: newprojectpanel
        {
            List<kompetenciak> list = mySql.Kompetenciak_MySql_listQuery("SELECT * FROM kompetenciak");
            mySql.close();
            return list;
        }

        public List<kompetencia_summary_struct> Data_KompetenciaJeloltKapcs() // javítva
        {
            string command = "SELECT coalesce(AVG(k1_val),0) as k1_val,coalesce(AVG(k2_val),0) as k2_val,coalesce(AVG(k3_val),0) as k3_val,coalesce(AVG(k4_val),0) as k4_val,coalesce(AVG(k5_val),0) as k5_val, tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + aControl.ApplicantID+" AND projekt_id = "+pControl.ProjektID+"";
            List < kompetencia_summary_struct > list = mySql.Kompetencia_summary_MySql_listQuery(command);
            mySql.close();
            return list;
        }
        
        public List<kompetencia_tamogatas> Data_KompetenciaTamogatas() // javítva
        {
            string command = "SELECT tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + aControl.ApplicantID + " AND projekt_id = " + pControl.ProjektID + "";
            List<kompetencia_tamogatas> list = mySql.Kompetencia_tamogatas_MySql_listQuery(command);
            mySql.close();
            return list;
        }

        public void kompetenciaUpdate(List<int> list) // javítva használja: interviewpanel
        {
            string command = "INSERT INTO `kompetencia_jelolt_kapcs` (`id`, `interju_id`, `projekt_id`, `jelolt_id`, `hr_id`, `k1_id`, `k1_val`, `k2_id`, `k2_val`, `k3_id`, `k3_val`, `k4_id`, `k4_val`, `k5_id`, `k5_val`, tamogatom) VALUES (null, " + InterjuID + ", " + pControl.ProjektID + ", " + aControl.ApplicantID + ", " + session.UserData[0].id + ", "+list[0]+ ", " + list[1] + ", " + list[2] + ", " + list[3] + ", " + list[4] + ", " + list[5] + ", " + list[6] + ", " + list[7] + ", " + list[8] + ", " + list[9] + ", " + list[10] + ");";
            mySql.update(command);
            mySql.close();
        }

        public bool hasKompetencia() // javítva
        {
            string command = "SELECT * FROM kompetencia_jelolt_kapcs WHERE interju_id = "+ InterjuID + " AND hr_id = "+session.UserData[0].id+"";
            bool response = mySql.bind(command);
            mySql.close();
            return response;
        }

        public List<ertesitendok_struct> Data_ProjektErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON ertesitendok_id = users.id WHERE projekt_id = "+pControl.ProjektID+"";
            List<ertesitendok_struct> list = mySql.getErtesitendok(command);
            mySql.close();
            return list;
        }

        public List<ertesitendok_struct> Data_InterjuErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email FROM interju_resztvevo_kapcs left JOIN users ON user_id = users.id WHERE interju_id = " + InterjuID + " GROUP BY users.id";
            List<ertesitendok_struct> list = mySql.getErtesitendok(command);
            mySql.close();
            return list;
        }

        public void insertInterviewInvited(int id) // javítva
        {
            string command = "INSERT INTO `interju_resztvevo_kapcs` (`id`, `interju_id`, `user_id`) VALUES (NULL, "+InterjuID+", "+id+");";
            mySql.update(command);
            mySql.close();
        }

        public void deleteInterviewInvited(int id) // javítva
        {
            string command = "DELETE FROM `interju_resztvevo_kapcs` WHERE user_id = " + id + "";
            mySql.update(command);
            mySql.close();
        }
        
        //public void AddInterPlusOne()
        //{
        //    string command = "UPDATE projekt_jelolt_kapcs SET allapot = allapot + 1 WHERE projekt_id = "+pcontrol.ProjektID+ " AND jelolt_id = "+acontrol.ApplicantID+"";
        //    mysql.update(command);
        //}

        //public void progress_delete()
        //{
        //    string command = "UPDATE projekt_jelolt_kapcs SET allapot = allapot - 1 WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
        //    mysql.update(command);
        //}

        //public void Telefon_Szures_Elutasit()
        //{
        //    string command1 = "UPDATE projekt_jelolt_kapcs SET telefonos_szures = 0  WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
        //    string command2 = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
        //    string command3 = "DELETE FROM megjegyzesek WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
        //    string command4 = "DELETE FROM interjuk_kapcs WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";

        //    mysql.update(command1);
        //    mysql.update(command2);
        //    mysql.update(command3);
        //    mysql.update(command4);
        //}
    }
}
