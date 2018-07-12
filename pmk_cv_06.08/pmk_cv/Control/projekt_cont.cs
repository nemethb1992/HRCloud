using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Control
{
    class projekt_cont
    {
        private static bool Changes;
        public bool Change { get { return Changes; } set { Changes = value; } }

        private static int ProjektIDs;
        public int ProjektID { get { return ProjektIDs; } set { ProjektIDs = value; } }

        private static List<Projekt_Search_Memory> Projekt_Search_Memory;
        public List<Projekt_Search_Memory> projekt_search_memory { get { return Projekt_Search_Memory; } set { Projekt_Search_Memory = value; } }

        dbEntities dbE = new dbEntities();
        Session sess = new Session();
        applicant_cont acontrol = new applicant_cont();
        public List<ProjectListItems> ProjektListSource(List<string> list)
        {
            string query = "SELECT coalesce((SELECT count(jelolt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id),0) as jeloltek_db, coalesce((SELECT count(jelolt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id),0) as interjuk_db, projektek.id, projektek.publikalt, megnevezes_projekt, megnevezes_munka, fel_datum, statusz FROM projektek LEFT JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id LEFT JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id LEFT JOIN munkakor ON munkakor.id = projektek.munkakor LEFT JOIN pc ON pc.id = projektek.pc LEFT JOIN megjegyzesek ON projektek.id = megjegyzesek.projekt_id " +
                " WHERE projektek.statusz = " + projekt_search_memory[0].statusz;

       //     if (list[0] != "" || list[1] != "0" || list[2] != "" || list[3] != "0" || list[4] != "" || list[5] != "" || list[6] != "" || list[7] != "" || list[8] != "")
       //     {
       //         string search = " AND projektek.megnevezes_projekt LIKE '%" + list[0] + "%'" +
       //" AND coalesce((SELECT count(projekt_id)  FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id Group by projekt_id),0) >=" + list[1] +
       //" AND projektek.fel_datum LIKE '%" + list[2] + "%'" +
       //" AND coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id Group by jelolt_id),0) >=  " + list[3] +
       //" AND pc.megnevezes_pc LIKE '%" + list[4] + "%'" +
       //" AND projektek.nyelvtudas LIKE '%" + list[5] + "%'" +
       //" AND projektek.vegzettseg LIKE '%" + list[6] + "%'";
                if (list[0] != "")
                {
                    query += " AND projektek.megnevezes_projekt LIKE '%" + list[0] + "%' ";
                }
                if (list[1] != "0")
                {
                    query += " AND coalesce((SELECT count(projekt_id)  FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id Group by projekt_id),0) >=" + list[1] + " ";
                }
                if (list[2] != "")
                {
                    query += " AND projektek.fel_datum LIKE '%" + list[2] + "%' ";
                }
                if (list[3] != "0")
                {
                    query += " AND coalesce((SELECT count(jelolt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id Group by jelolt_id),0) >=" + list[3] + " ";
                }
                if (list[4] != "")
                {
                    query += " AND pc.megnevezes_pc LIKE '%" + list[4] + "%' ";
                }
                if (list[5] != "" && list[5] != "1")
                {
                    query += " AND projektek.nyelvtudas LIKE '%" + list[5] + "%' ";
                }
                if (list[6] != "" && list[6] != "1")
                {
                    query += " AND projektek.vegzettseg LIKE '%" + list[6] + "%' ";
                }
                if (list[7] != "")
                {
                    query += " AND megjegyzesek.megjegyzes LIKE '%" + list[7] + "%' ";
                }
                if (list[8] != "")
                {
                    query += " AND jeloltek.nev LIKE '%" + list[8] + "%' ";
                }
            if (list[9] != "")
            {
                query += " AND projektek.publikalt LIKE '%" + list[9] + "%' ";
            }
            query += " GROUP BY projektek.id";
            return dbE.Projekt_MySql_listQuery(query);
        }
        public List<ProjectExtendedListItems> ProjektFullDataSource()
        {
            string query = "SELECT (SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id Group by projekt_id) as jeloltek_db, " +
                "projektek.id, projektek.hr_id, megnevezes_projekt, megnevezes_vegzettseg, megnevezes_nyelv,megnevezes_munka,megnevezes_pc,name,fel_datum,le_datum,pc,vegzettseg,tapasztalat_ev,allapot,nyelvtudas,munkakor,szuldatum,ber,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5,feladatok,elvarasok,kinalunk, elonyok, publikalt  " +
                "FROM projektek " +
                "LEFT JOIN munkakor on munkakor.id = projektek.munkakor " +
                "LEFT JOIN nyelv ON nyelv.id = projektek.nyelvtudas " +
                "LEFT JOIN vegzettsegek ON vegzettsegek.id = projektek.vegzettseg " +
                "LEFT JOIN users ON users.id = projektek.hr_id " +
                "LEFT JOIN pc ON pc.id = projektek.pc " +
                "LEFT JOIN statusz ON projektek.statusz = statusz.id " +
                "WHERE projektek.id = " + ProjektID + " GROUP BY projektek.id";
            return dbE.Projekt_Extended_MySql_listQuery(query);
        }
        public List<JeloltListItems> JeloltListSourceForListBox()
        {
            string query = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id Group by projekt_id),0) as interjuk_db, jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota, kolcsonzott FROM jeloltek INNER JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id INNER JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id INNER JOIN munkakor ON jeloltek.munkakor = munkakor.id WHERE projektek.id =" + ProjektID + " GROUP BY jeloltek.id ";
            return dbE.Jelolt_MySql_listQuery(query);
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
        public List<megjegyzes_struct> megjegyzes_datasource()
        {
            string query = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + ProjektID;
            return dbE.Megjegyzesek_MySql_listQuery(query);
        }
        public List<megjegyzes_struct> megjegyzes_datasource_kapcsolati()
        {
            string query = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + ProjektID +" AND jelolt_id="+acontrol.ApplicantID+"";
            return dbE.Megjegyzesek_MySql_listQuery(query);
        }
        //public List<csatolmany_struct> CsatolmanyDataSource()
        //{
        //    string query = "SELECT projektek.id, kapcs_id, megnevezes_projekt, tipus, fajlnev, kiterjesztes FROM csatolmanyok INNER JOIN projektek ON csatolmanyok.kapcs_id = projektek.id WHERE projektek.id = " + ProjektID + " AND tipus=1";
        //    return dbE.Csatolmany_MySql_listQuery(query);
        //}
        public List<ertesitendok_struct> ErtesitendokDataSource_cbx(string ertesitendok_src)
        {
            string query = "SELECT id, name ,email FROM users WHERE name LIKE '%"+ertesitendok_src+"%' AND kategoria = 0";
            return dbE.Ertesitendok_MySql_listQuery(query);
        }
        public List<hr_struct> HrDataSource_cbx(string nev_src)
        {
            string query = "SELECT id, name, kategoria, jogosultsag, validitas FROM users WHERE name LIKE '%" + nev_src + "%' AND kategoria = 1 GROUP BY users.name";
            return dbE.HR_rovid_MySql_listQuery(query);
        }
        public List<hr_struct> HrDataSource_toProjektList()
        {
            string query = "SELECT users.id, name, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_hr_kapcs ON users.id = projekt_hr_kapcs.hr_id INNER JOIN projektek ON projektek.id = projekt_hr_kapcs.projekt_id WHERE projektek.id = " + ProjektID + " AND users.kategoria = 1 GROUP BY users.id";
            return dbE.HR_rovid_MySql_listQuery(query);
        }
      
        public List<ertesitendok_struct> ErtesitendokDataSource_toProjektList()
        {
            string query = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON users.id = projekt_ertesitendok_kapcs.ertesitendok_id  WHERE projekt_ertesitendok_kapcs.projekt_id =" + ProjektID+ " AND kategoria = 0 GROUP BY users.id";
            return dbE.Ertesitendok_MySql_listQuery(query);
        }
        public List<SubJelolt> JeloltDataSource_cbx(string nevsrc)
        {
            string query = "SELECT jeloltek.id, nev FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id WHERE projekt_jelolt_kapcs.projekt_id != "+ProjektID+" OR projekt_jelolt_kapcs.projekt_id IS NULL GROUP BY jeloltek.id";
            return dbE.Jelolt_Short_DataSource(query);
        }
        public void Jelolt_write_to_project(int jelolt_index, int projekt_index)
        {
            string query1 = "SELECT * FROM projekt_jelolt_kapcs";
            DateTime dateTime = DateTime.Now;
            List<projekt_jelolt_kapcs> list =  dbE.Jelolt_Projekt_kapcs_MySql_listQuery(query1);
            bool notexist = true;
            foreach (var i in list)
            {
                if (i.projekt_id == projekt_index && i.jelolt_id == jelolt_index)
                {
                    notexist = false;
                }
            }
            if(notexist)
            {
                string query2 = "INSERT INTO projekt_jelolt_kapcs (id, projekt_id, jelolt_id, hr_id, datum) VALUES (NULL, " + projekt_index + ", " + jelolt_index + ", " + sess.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "' );";
                dbE.MysqlQueryExecute(query2);
            }
        }
        public void HR_write_to_project(int index)
        {
            string query = "INSERT INTO projekt_hr_kapcs (id, projekt_id, hr_id) VALUES (NULL, " + ProjektID + ", " + index + " );";
            dbE.MysqlQueryExecute(query);
        }
        public void Ertesitendok_write_to_project(int index)
        {
            string query = "INSERT INTO projekt_ertesitendok_kapcs (id, projekt_id, ertesitendok_id) VALUES (NULL, " + ProjektID + ", " + index + " );";
            dbE.MysqlQueryExecute(query);
        }
        public void Jelolt_list_delete(int id)
        {
            string query = "DELETE FROM projekt_jelolt_kapcs WHERE jelolt_id = "+id+" AND projekt_id = " + ProjektID + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void Jelolt_list_allpot_UPDATE(int id, int allapota)
        {
            string query = "UPDATE projekt_jelolt_kapcs SET allapota = "+allapota+" WHERE jelolt_id = " + id + " AND projekt_id = " + ProjektID + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void Ertesitendok_list_delete(int id)
        {
            string query = "DELETE FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + id + " AND projekt_id = " + ProjektID + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void HR_list_delete(int id)
        {
            string query = "DELETE FROM projekt_hr_kapcs WHERE hr_id = " + id + " AND projekt_id = " + ProjektID + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void Projekt_publikal(int stat)
        {
            string query = "UPDATE projektek SET publikalt= "+ stat + " WHERE projektek.id = " + ProjektID + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void Projekt_Archiver(int id, int statusz)
        {
            if (statusz == 0)
            {
                statusz = 1;
            }
            else
            {
                statusz = 0;
            }
            string query = "UPDATE projektek SET statusz="+ statusz + " WHERE projektek.id = " + id + ";";
            dbE.MysqlQueryExecute(query);
        }
        public void Projekt_delete(int id)
        {
            string query = "DELETE FROM projektek WHERE projektek.id = " + id + ";";
            dbE.MysqlQueryExecute(query);
            string query2 = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.projekt_id = " + id + ";";
            dbE.MysqlQueryExecute(query2);
            string query3 = "DELETE FROM projekt_hr_kapcs WHERE projekt_hr_kapcs.projekt_id = " + id + ";";
            dbE.MysqlQueryExecute(query3);
            string query4 = "DELETE FROM projekt_ertesitendok_kapcs WHERE projekt_ertesitendok_kapcs.projekt_id = " + id + ";";
            dbE.MysqlQueryExecute(query4);
            string query5 = "DELETE FROM megjegyzesek WHERE megjegyzesek.projekt_id = " + id + ";";
            dbE.MysqlQueryExecute(query5);
            string query6 = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id = " + id + ";";
            dbE.MysqlQueryExecute(query6);
            string query7 = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.projekt_id = " + id + ";";
            dbE.MysqlQueryExecute(query7);
            string query8 = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id=" + id + " AND hr_id=" + sess.UserData[0].id + "";
            dbE.MysqlQueryExecute(query8);
        }
        public void Projekt_list_INSERT(List<ProjectInsertListItems> items)
        {
            string query = "INSERT INTO projektek (`id`, `hr_id`, `megnevezes_projekt`, `pc`, `vegzettseg`, `tapasztalat_ev`, `statusz`, `fel_datum`, `le_datum`, `nyelvtudas`, `munkakor`, `szuldatum`, `ber`,  `kepesseg1`, `kepesseg2`, `kepesseg3`, `kepesseg4`, `kepesseg5`, `feladatok`, `elvarasok`, `kinalunk`)" +
                " VALUES (NULL, "+items[0].hr_id+ ", '" + items[0].megnevezes_projekt+ "'," + items[0].pc+ "," + items[0].vegzettseg+ "," + items[0].tapasztalat_ev+ "," + items[0].statusz+ ",'" + items[0].fel_datum+ "','" + items[0].le_datum+ "'," + items[0].nyelvtudas+ "," + items[0].munkakor+ "," + items[0].szuldatum + "," + items[0].ber+ "," + items[0].kepesseg1+ "," + items[0].kepesseg2+ "," + items[0].kepesseg3+ "," + items[0].kepesseg4+ "," + items[0].kepesseg5+ ",'" + items[0].feladatok+ "','" + items[0].elvarasok+ "','" + items[0].kinalunk+ "');";
            dbE.MysqlQueryExecute(query);
            int proID = Convert.ToInt16(dbE.MysqlReaderExecute_List("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            ProjektID = proID;
        }
        public void Projekt_list_UPDATE(List<ProjectInsertListItems> items)
        {
            string query = "UPDATE projektek SET " +
                " `hr_id` =  " + items[0].hr_id + ", " +
                "`megnevezes_projekt` =  '" + items[0].megnevezes_projekt + "', " +
                " `pc` =  " + items[0].pc + ", " +
                "`vegzettseg` =  " + items[0].vegzettseg + ", " +
                "`tapasztalat_ev` =  " + items[0].tapasztalat_ev + ", " +
                "`statusz` =  " + items[0].statusz + ", " +
                "`nyelvtudas` =  " + items[0].nyelvtudas + ", " +
                "`munkakor` =  " + items[0].munkakor + ", " +
                "`szuldatum` =  " + items[0].szuldatum + ", " +
                "`ber` =  " + items[0].ber + ", " +
                "`kepesseg1` =  " + items[0].kepesseg1 + ", " +
                "`kepesseg2` =  " + items[0].kepesseg2 + ", " +
                "`kepesseg3` =  " + items[0].kepesseg3 + ", " +
                "`kepesseg4` =  " + items[0].kepesseg4 + ", " +
                "`kepesseg5` =  " + items[0].kepesseg5 + " WHERE id = "+ ProjektID + "";
            dbE.MysqlQueryExecute(query);
            int proID = Convert.ToInt16(dbE.MysqlReaderExecute_List("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            ProjektID = proID;
        }
        public void Projekt_allapot_valto(int stat)
        {
            if(projekt_search_memory != null)
            projekt_search_memory.Clear();
            //List<Projekt_Search_Memory> list = new List<Projekt_Search_Memory>();
            projekt_search_memory.Add(new Projekt_Search_Memory() { statusz = stat });
            //projekt_search_memory = list;
        }
        public void Projekt_Leiras_Update(string type, string content)
        {
            string query = "";
            switch (type)
            {
                case "feladatok":
                    query = "UPDATE projektek SET feladatok='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = "+sess.UserData[0].id+"";
                    break;
                case "elvarasok":
                    query = "UPDATE projektek SET elvarasok='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = " + sess.UserData[0].id + "";
                    break;
                case "kinalunk":
                    query = "UPDATE projektek SET kinalunk='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = " + sess.UserData[0].id + "";
                    break;
                case "elonyok":
                    query = "UPDATE projektek SET elonyok='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = " + sess.UserData[0].id + "";
                    break;
                default:
                    break;
            }
            dbE.MysqlQueryExecute(query);
        }
        public List<koltsegek> Projekt_Koltseg_Query()
        {
            string query = "SELECT * FROM projekt_koltsegek WHERE projekt_id = "+ProjektID+"";
            return dbE.Koltsegek_MySql_listQuery(query);
        }
        public void Projekt_Koltseg_Insert(string megnevezes, string osszeg)
        {
            string query = "INSERT INTO `projekt_koltsegek` (id, projekt_id, koltseg_megnevezes, osszeg) VALUES (null, "+ProjektID+", '"+megnevezes+"', "+osszeg+");";
            dbE.MysqlQueryExecute(query);
        }
        public void Projekt_Koltseg_Delete(int id)
        {
            string query = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.id = " + id + "";
            dbE.MysqlQueryExecute(query);
        }
    }
}
