using HRCloud.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRCloud.Control
{
    class applicant_cont
    {
        Session sess = new Session();
        dbEntities dbE = new dbEntities();
        ftpEntities ftpE = new ftpEntities();


        private static bool Changes;
        public bool Change { get { return Changes; } set { Changes = value; } }

        private static int ApplicantIDs;
        public int ApplicantID { get { return ApplicantIDs; } set { ApplicantIDs = value; } }

        public List<JeloltListItems> JeloltListSource(List<string> list)
        {

            string query = "SELECT " +
                "coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id GROUP BY jelolt_id),0) as interjuk_db, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor) as munkakor, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2) as munkakor2, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3) as munkakor3, " +
                "jeloltek.id,jeloltek.nev,szuldatum,reg_date,allapota,kolcsonzott,email " +
                "FROM jeloltek " +
                "LEFT JOIN megjegyzesek ON jeloltek.id = megjegyzesek.jelolt_id " +
                "LEFT JOIN munkakor on jeloltek.munkakor = munkakor.id " +
                "LEFT JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id LIKE '%%'";
            
            if (list[0] != "")
            {
                query += " AND jeloltek.nev LIKE '%" + list[0] + "%' ";
            }
            if (list[1] != "")
            {
                query += " AND jeloltek.lakhely LIKE '%" + list[1] + "%' ";
            }
            if (list[2] != "")
            {
                query += " AND jeloltek.email LIKE '%" + list[2] + "%' ";
            }
            if (list[3] != "")
            {
                query += " AND jeloltek.szuldatum <= " + list[3] + " ";
            }
            if (list[4] != "" && list[4] != "0")
            {
                query += "AND jeloltek.tapasztalat_ev >= " + list[4] + " ";
            }
            if (list[5] != "")
            {
                query += " AND jeloltek.reg_date LIKE '%" + list[5] + "%' ";
            }
            if (list[6] != "" && list[6] != "0")
            {
                query += " AND coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id Group by projekt_id),0) >= " + list[6] + " ";
            }
            if (list[7] != "")
            {
                query += " AND jeloltek.neme LIKE '%" + list[7] + "%' ";
            }
            if (list[8] != "")
            {
                query += " AND jeloltek.munkakor LIKE '%" + list[8] + "%' ";
            }
            if (list[9] != "")
            {
                query += " AND jeloltek.vegz_terulet LIKE '%" + list[9] + "%' ";
            }
            if (list[10] != "")
            {
                query += " AND megjegyzesek.megjegyzes LIKE '%" + list[10] + "%' ";
            }
            if (list[11] == "1")
            {
                query += "  AND projekt_jelolt_kapcs.id IS NULL ";
            }
            query += " GROUP BY jeloltek.email ";
            switch (list[12])
            {
                case "1":
                    query += " ORDER BY jeloltek.id" + list[13];
                    break;
                case "2":
                    query += " ORDER BY jeloltek.nev" + list[13];
                    break;
                case "3":
                    query += " ORDER BY jeloltek.munkakor" + list[13];
                    break;
                case "4":
                    query += " ORDER BY interjuk_db" + list[13];
                    break;
                case "5":
                    query += " ORDER BY jeloltek.szuldatum" + list[13];
                    break;
                case "6":
                    query += " ORDER BY jeloltek.reg_date" + list[13];
                    break;
                default:
                    query += " ORDER BY jeloltek.reg_date DESC";
                    break;
            }
            return dbE.Jelolt_MySql_listQuery(query);
        }

        public List<JeloltExtendedList> JeloltFullDataSource()
        {
            string query = "SELECT jeloltek.id,nev,email,telefon,lakhely,pmk_ismerte,szuldatum,neme,tapasztalat_ev, reg_date,felvett,jeloltek.megjegyzes,folderUrl,hirlevel," +
                "coalesce((SELECT nem FROM nemek WHERE nemek.id = jeloltek.neme),'') AS neme," +
                "(SELECT nemek.id FROM nemek WHERE nemek.id = jeloltek.neme) AS id_neme," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor),'') AS munkakor," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),'') AS munkakor2," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),'') AS munkakor3," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor),0) AS id_munkakor," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),0) AS id_munkakor2," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),0) AS id_munkakor3," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),'') AS nyelvtudas," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),'') AS nyelvtudas2," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),0) AS id_nyelvtudas," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),0) AS id_nyelvtudas2," +
                "coalesce((SELECT ertesules_megnevezes FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),'') AS ertesules_megnevezes, " +
                "coalesce((SELECT ertesulesek.id FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),0) AS id_ertesult, " +
                "coalesce((SELECT megnevezes_vegzettseg FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),'') AS vegz_terulet, " +
                "coalesce((SELECT vegzettsegek.id FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),0) AS id_vegz_terulet " +
                "FROM jeloltek WHERE jeloltek.id = " + ApplicantID + "";
            //string query = "SELECT * FROM jeloltek INNER JOIN nyelv on jeloltek.nyelvtudas = nyelv.id INNER JOIN munkakor on jeloltek.munkakor = munkakor.id INNER JOIN ertesulesek ON jeloltek.ertesult = ertesulesek.id WHERE jeloltek.id = " + ApplicantID + "";
            return dbE.JeloltExtended_MySql_listQuery(query);
        }
        public List<munkakor_struct> MunkakorDataSource()
        {
            string query = "SELECT * FROM munkakor";
            return dbE.Munkakor_MySql_listQuery(query);
        }
        public List<statusz_struct> StatuszDataSource()
        {
            string query = "SELECT * FROM statusz";
            return dbE.Statusz_MySql_listQuery(query);
        }
        public List<pc_struct> PCataSource()
        {
            string query = "SELECT * FROM pc";
            return dbE.PC_MySql_listQuery(query);
        }
        public List<vegzettseg_struct> VegzettsegDataSource()
        {
            string query = "SELECT * FROM vegzettsegek";
            return dbE.Vegzettseg_MySql_listQuery(query);
        }
        public List<nyelv_struct> NyelvDataSource()
        {
            string query = "SELECT * FROM nyelv";
            return dbE.Nyelv_MySql_listQuery(query);
        }
        public List<ertesulesek> ErtesulesekDataSource()
        {
            string query = "SELECT * FROM ertesulesek";
            return dbE.Ertesulesek_MySql_listQuery(query);
        }
        public List<neme_struct> NemekDatasource()
        {
            string query = "SELECT * FROM nemek";
            return dbE.Nem_MySql_listQuery(query);
        }
        //public List<csatolmany_struct> CsatolmanyDataSource()
        //{
        //    string query = "SELECT jeloltek.id, jeloltek.nev, tipus, fajlnev,kapcs_id, kiterjesztes FROM csatolmanyok INNER JOIN jeloltek ON csatolmanyok.kapcs_id = jeloltek.id WHERE jeloltek.id = " + ApplicantID + " AND tipus=0";
        //    return dbE.Csatolmany_MySql_listQuery(query);
        //}
        public bool CvDownload(string nev, string fajlnev, string kiterjesztes)
        {
            string url = "applicant_cv";
            return ftpE.ftpFileDownload(nev, url, fajlnev, kiterjesztes);
        }
        public bool CvUpload(string path)
        {
            bool respond = false;
            string nev = dbE.MysqlReaderExecute_List("Select nev from jeloltek where id=" + ApplicantID + "", "jeloltek", 1)[0];
            string url = "applicant_cv";

            List<ftp_feltoltes> li = ftpE.ftpFileUpload(path, url, ApplicantID.ToString(), nev);
            try
            {
                if (li[0].allapot)
                {
                    string fajlnev = "HRcloud_" + nev.Replace(' ', '_') + "_cv." + li[0].kiterjesztes;
                    string query = "INSERT INTO csatolmanyok (kapcs_id,tipus,fajlnev,kiterjesztes) VALUES (" + ApplicantID + ",0,'" + fajlnev + "','" + li[0].kiterjesztes + "')";
                    dbE.MysqlQueryExecute(query);
                    respond = true;
                }
            }
            catch (Exception)  { }
            return respond;
        }
        public List<megjegyzes_struct> megjegyzes_datasource()
        {
            string query = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE jelolt_id=" + ApplicantID;
            return dbE.Megjegyzesek_MySql_listQuery(query);
        }
        public List<SmallProjectListItems> ProjektListSourceForListBox()
        {
            string query = "SELECT projektek.id, megnevezes_projekt FROM projektek INNER JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id INNER JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id WHERE jeloltek.id = "+ApplicantID+" GROUP BY projektek.id";
            return dbE.Small_Projekt_MySql_listQuery(query);
        }
        public List<SmallProjectListItems> Small_Projekt_list()
        {
            string query = "SELECT projektek.id, megnevezes_projekt FROM projektek WHERE statusz = 1";
            return dbE.Small_Projekt_MySql_listQuery(query);
        }
        public void projekt_list_delete(int id)
        {
            string query = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + ApplicantID + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void Jelolt_delete(int id)
        {
            string query = "DELETE FROM jeloltek WHERE jeloltek.id = " + id + ";";
            dbE.MysqlQueryExecute(query);
            string query2 = "DELETE FROM kepessegek WHERE kepessegek.jelolt_id = " + id + ";";
            dbE.MysqlQueryExecute(query2);
            string query3 = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = " + id + ";";
            dbE.MysqlQueryExecute(query3);
            string query4 = "DELETE FROM megjegyzesek WHERE megjegyzesek.jelolt_id = " + id + ";";
            dbE.MysqlQueryExecute(query4);
        }
        public void Jelolt_list_INSERT(List<JeloltExtendedList> items)
        {
            string query = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, neme, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`) " +
                "VALUES(NULL, '" + items[0].nev + "',  '" + items[0].email + "', '" + items[0].telefon + "', '" + items[0].lakhely + "', " + items[0].ertesult + ", " + items[0].szuldatum + ", " + items[0].neme + "," + items[0].tapasztalat_ev + "," + items[0].munkakor + "," + items[0].munkakor2 + "," + items[0].munkakor3 + "," + items[0].vegz_terulet + "," + items[0].nyelvtudas + "," + items[0].nyelvtudas2 + ",'" + items[0].reg_date +"');";
            dbE.MysqlQueryExecute(query);
            int appID = Convert.ToInt16(dbE.MysqlReaderExecute_List("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "' AND jeloltek.lakhely = '" + items[0].lakhely + "'", "jeloltek", 1)[0]);
            ApplicantID = appID;
        }
        public void Jelolt_list_UPDATE(List<JeloltExtendedList> items)
        {
            string query = "UPDATE jeloltek SET "+
                " `nev` = '"+items[0].nev+"'" +
                ", `email` = '" + items[0].email + "'" +
                ", `telefon` = '" + items[0].telefon + "'" +
                ", `lakhely` = '" + items[0].lakhely + "'" +
                ", `ertesult` = " + items[0].ertesult + "" +
                ", `szuldatum` = '" + items[0].szuldatum + "'" +
                ", `neme` = " + items[0].neme + "" +
                ", `tapasztalat_ev` = " + items[0].tapasztalat_ev + "" +
                ", `munkakor` = " + items[0].munkakor + "" +
                ", `munkakor2` = " + items[0].munkakor2 + "" +
                ", `munkakor3` = " + items[0].munkakor3 + "" +
                ", `vegz_terulet` = " + items[0].vegz_terulet + "" +
                ", `nyelvtudas` = " + items[0].nyelvtudas + "" +
                ",`nyelvtudas2` = " + items[0].nyelvtudas2 + "" +
                ", `reg_date`  = '" + items[0].reg_date + "'" +
                "WHERE jeloltek.id = " +ApplicantID+"";
            dbE.MysqlQueryExecute(query);
            int appID = Convert.ToInt16(dbE.MysqlReaderExecute_List("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "' AND jeloltek.lakhely = '" + items[0].lakhely + "'", "jeloltek", 1)[0]);
            ApplicantID = appID;
        }
    }
}