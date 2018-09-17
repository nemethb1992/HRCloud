using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Control
{
    class ControlProject
    {
        private static bool Changes;
        public bool Change { get { return Changes; } set { Changes = value; } }

        private static int ProjektIDs;
        public int ProjektID { get { return ProjektIDs; } set { ProjektIDs = value; } }

        private static List<Projekt_Search_Memory> ProjectSearchMemory;
        public List<Projekt_Search_Memory> projectSearchMemory { get { return ProjectSearchMemory; } set { ProjectSearchMemory = value; } }

        Model.MySql MySql = new Model.MySql();
        Session sess = new Session();
        ControlApplicant acontrol = new ControlApplicant();
        public List<ProjectListItems> Data_ProjectFull(List<string> list)
        {
            string command = "SELECT coalesce((SELECT count(jelolt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id GROUP BY jeloltek.id),0) as jeloltek_db, coalesce((SELECT count(jelolt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id),0) as interjuk_db, projektek.id, projektek.publikalt, megnevezes_projekt, megnevezes_munka, fel_datum, statusz FROM projektek LEFT JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id LEFT JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id LEFT JOIN munkakor ON munkakor.id = projektek.munkakor LEFT JOIN pc ON pc.id = projektek.pc LEFT JOIN megjegyzesek ON projektek.id = megjegyzesek.projekt_id " +
            " WHERE projektek.statusz = " + projectSearchMemory[0].statusz;
            if (list[0] != "")
            {
                command += " AND projektek.megnevezes_projekt LIKE '%" + list[0] + "%' ";
            }
            if (list[1] != "0")
            {
                command += " AND coalesce((SELECT count(projekt_id)  FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id Group by projekt_id),0) >=" + list[1] + " ";
            }
            if (list[2] != "")
            {
                command += " AND projektek.fel_datum LIKE '%" + list[2] + "%' ";
            }
            if (list[3] != "0")
            {
                command += " AND coalesce((SELECT count(jelolt_id) FROM interjuk_kapcs WHERE projekt_id = projektek.id Group by jelolt_id),0) >=" + list[3] + " ";
            }
            if (list[4] != "")
            {
                command += " AND pc.megnevezes_pc LIKE '%" + list[4] + "%' ";
            }
            if (list[5] != "" && list[5] != "1")
            {
                command += " AND projektek.nyelvtudas LIKE '%" + list[5] + "%' ";
            }
            if (list[6] != "" && list[6] != "1")
            {
                command += " AND projektek.vegzettseg LIKE '%" + list[6] + "%' ";
            }
            if (list[7] != "")
            {
                command += " AND megjegyzesek.megjegyzes LIKE '%" + list[7] + "%' ";
            }
            if (list[8] != "")
            {
                command += " AND jeloltek.nev LIKE '%" + list[8] + "%' ";
            }
            if (list[9] != "")
            {
                command += " AND projektek.publikalt LIKE '%" + list[9] + "%' ";
            }
            command += " GROUP BY projektek.id ";
            switch (list[10])
            {
                case "1":
                    command += " ORDER BY projektek.id" + list[11];
                    break;
                case "2":
                    command += " ORDER BY projektek.megnevezes_projekt" + list[11];
                    break;
                case "3":
                    command += " ORDER BY projektek.munkakor" + list[11];
                    break;
                case "4":
                    command += " ORDER BY jeloltek_db" + list[11];
                    break;
                case "5":
                    command += " ORDER BY projektek.fel_datum" + list[11];
                    break;
                default:
                    command += " ORDER BY projektek.fel_datum DESC";
                    break;
            }

            return MySql.Projekt_MySql_listQuery(command);
        }
        public List<ProjectExtendedListItems> Data_ProjectFull()
        {
            string command = "SELECT (SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id GROUP BY projekt_id) as jeloltek_db, " +
                "projektek.id, projektek.hr_id, megnevezes_projekt, megnevezes_vegzettseg, megnevezes_nyelv,megnevezes_munka,megnevezes_pc,name,fel_datum,le_datum,pc,vegzettseg,tapasztalat_ev,allapot,nyelvtudas,munkakor,szuldatum,ber,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5,feladatok,elvarasok,kinalunk, elonyok, publikalt  " +
                "FROM projektek " +
                "LEFT JOIN munkakor on munkakor.id = projektek.munkakor " +
                "LEFT JOIN nyelv ON nyelv.id = projektek.nyelvtudas " +
                "LEFT JOIN vegzettsegek ON vegzettsegek.id = projektek.vegzettseg " +
                "LEFT JOIN users ON users.id = projektek.hr_id " +
                "LEFT JOIN pc ON pc.id = projektek.pc " +
                "LEFT JOIN statusz ON projektek.statusz = statusz.id " +
                "WHERE projektek.id = " + ProjektID + " GROUP BY projektek.id";
            return MySql.Projekt_Extended_MySql_listQuery(command);
        }
        public List<JeloltListItems> Data_jeloltKapcs()
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id AND projekt_id = " + ProjektID + " Group by projekt_id),0) as interjuk_db, jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,email,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota, kolcsonzott FROM jeloltek INNER JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id INNER JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id INNER JOIN munkakor ON jeloltek.munkakor = munkakor.id WHERE projektek.id =" + ProjektID + " GROUP BY jeloltek.id ";
            return MySql.Jelolt_MySql_listQuery(command);
        }
        public List<vegzettseg_struct> Data_Vegzettseg()
        {
            string command = "SELECT * FROM vegzettsegek";
            return MySql.Vegzettseg_MySql_listQuery(command);
        }
        public List<nyelv_struct> Data_Nyelv()
        {
            string command = "SELECT * FROM nyelv";
            return MySql.Nyelv_MySql_listQuery(command);
        }
        public List<megjegyzes_struct> Data_CommentProject()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + ProjektID;
            return MySql.Megjegyzesek_MySql_listQuery(command);
        }
        public List<megjegyzes_struct> Data_CommentKapcs()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + ProjektID +" AND jelolt_id="+acontrol.ApplicantID+"";
            return MySql.Megjegyzesek_MySql_listQuery(command);
        }
        //public List<csatolmany_struct> CsatolmanyDataSource()
        //{
        //    string query = "SELECT projektek.id, kapcs_id, megnevezes_projekt, tipus, fajlnev, kiterjesztes FROM csatolmanyok INNER JOIN projektek ON csatolmanyok.kapcs_id = projektek.id WHERE projektek.id = " + ProjektID + " AND tipus=1";
        //    return dbE.Csatolmany_MySql_listQuery(query);
        //}
        public List<ertesitendok_struct> Data_ErtesitendokCheckbox(string ertesitendok_src)
        {
            string command = "SELECT id, name ,email FROM users WHERE name LIKE '%"+ertesitendok_src+"%' AND kategoria = 0";
            return MySql.getErtesitendok(command);
        }
        public List<hr_struct> Data_HrCheckbox(string nev_src)
        {
            string command = "SELECT id, name, kategoria, jogosultsag, validitas FROM users WHERE name LIKE '%" + nev_src + "%' AND kategoria = 1 GROUP BY users.name";
            return MySql.getHrShort(command);
        }
        public List<hr_struct> Data_HrProject()
        {
            string command = "SELECT users.id, name, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_hr_kapcs ON users.id = projekt_hr_kapcs.hr_id INNER JOIN projektek ON projektek.id = projekt_hr_kapcs.projekt_id WHERE projektek.id = " + ProjektID + " AND users.kategoria = 1 GROUP BY users.id";
            return MySql.getHrShort(command);
        }
      
        public List<ertesitendok_struct> Data_ErtesitendokKapcs() // javítva
        {
            string command = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON users.id = projekt_ertesitendok_kapcs.ertesitendok_id  WHERE projekt_ertesitendok_kapcs.projekt_id =" + ProjektID+ " AND kategoria = 0 GROUP BY users.id";
            return MySql.getErtesitendok(command);
        }
        public List<SubJelolt> Data_JeloltForCheckbox(string nevsrc)
        {
            string command = "SELECT jeloltek.id, nev FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id WHERE projekt_jelolt_kapcs.projekt_id != "+ProjektID+" OR projekt_jelolt_kapcs.projekt_id IS NULL GROUP BY jeloltek.id";
            return MySql.Jelolt_Short_DataSource(command);
        }
        public void addJeloltInsert(int jelolt_index, int projekt_index)
        {
            string command = "SELECT * FROM projekt_jelolt_kapcs";
            DateTime dateTime = DateTime.Now;
            List<projekt_jelolt_kapcs> list =  MySql.Jelolt_Projekt_kapcs_MySql_listQuery(command);
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
                string command2 = "INSERT INTO projekt_jelolt_kapcs (id, projekt_id, jelolt_id, hr_id, datum) VALUES (NULL, " + projekt_index + ", " + jelolt_index + ", " + sess.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "' );";
                MySql.update(command2);
            }
        }
        public void addHrInsert(int index)
        {
            string command = "INSERT INTO projekt_hr_kapcs (id, projekt_id, hr_id) VALUES (NULL, " + ProjektID + ", " + index + " );";
            MySql.update(command);
        }
        public void addErtesitendokInsert(int index)
        {
            string command = "INSERT INTO projekt_ertesitendok_kapcs (id, projekt_id, ertesitendok_id) VALUES (NULL, " + ProjektID + ", " + index + " );";
            MySql.update(command);
        }
        public void jeloltKapcsDelete(int id)
        {
            string command;
            command = "DELETE FROM projekt_jelolt_kapcs WHERE jelolt_id = "+id+" AND projekt_id = " + ProjektID + ";";
            MySql.update(command);
            command = "DELETE FROM interjuk_kapcs WHERE jelolt_id = " + id + " AND projekt_id = " + ProjektID + ";";
            MySql.update(command);
        }
        public void jeloltKapcsUpdate(int id, int allapota)
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = "+allapota+" WHERE jelolt_id = " + id + " AND projekt_id = " + ProjektID + ";";
            MySql.update(command);
        }
        public void ertesitendokKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + id + " AND projekt_id = " + ProjektID + ";";
            MySql.update(command);
        }
        public void hrKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_hr_kapcs WHERE hr_id = " + id + " AND projekt_id = " + ProjektID + ";";
            MySql.update(command);
        }
        public void publishProject(int stat)
        {
            string command = "UPDATE projektek SET publikalt= "+ stat + " WHERE projektek.id = " + ProjektID + ";";
            MySql.update(command);
        }
        public void projectArchiver(int id, int statusz) // javított
        {
            if (statusz == 0)
            {
                statusz = 1;
            }
            else
            {
                statusz = 0;
            }
            string command = "UPDATE projektek SET statusz="+ statusz + " WHERE projektek.id = " + id + ";";
            MySql.update(command);
        }
        public void projectDelete(int id) // javított
        {
            string command;
            command = "DELETE FROM projektek WHERE projektek.id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.projekt_id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM projekt_hr_kapcs WHERE projekt_hr_kapcs.projekt_id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM projekt_ertesitendok_kapcs WHERE projekt_ertesitendok_kapcs.projekt_id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.projekt_id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.projekt_id = " + id + ";";
            MySql.update(command);
            command = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id=" + id + " AND hr_id=" + sess.UserData[0].id + "";
            MySql.update(command);
        }
        public void projectInsert(List<ProjectInsertListItems> items) // javított newprojectpanel
        {
            string command = "INSERT INTO projektek (`id`, `hr_id`, `megnevezes_projekt`, `pc`, `vegzettseg`, `tapasztalat_ev`, `statusz`, `fel_datum`, `le_datum`, `nyelvtudas`, `munkakor`, `szuldatum`, `ber`,  `kepesseg1`, `kepesseg2`, `kepesseg3`, `kepesseg4`, `kepesseg5`, `feladatok`, `elvarasok`, `kinalunk`)" +
                " VALUES (NULL, "+items[0].hr_id+ ", '" + items[0].megnevezes_projekt+ "'," + items[0].pc+ "," + items[0].vegzettseg+ "," + items[0].tapasztalat_ev+ "," + items[0].statusz+ ",'" + items[0].fel_datum+ "','" + items[0].le_datum+ "'," + items[0].nyelvtudas+ "," + items[0].munkakor+ "," + items[0].szuldatum + "," + items[0].ber+ "," + items[0].kepesseg1+ "," + items[0].kepesseg2+ "," + items[0].kepesseg3+ "," + items[0].kepesseg4+ "," + items[0].kepesseg5+ ",'" + items[0].feladatok+ "','" + items[0].elvarasok+ "','" + items[0].kinalunk+ "');";
            MySql.update(command);
            int proID = Convert.ToInt16(MySql.MysqlReaderExecute_List("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            ProjektID = proID;
        }
        public void projectUpdate(List<ProjectInsertListItems> items) // javított newprojectpanel
        {
            string command = "UPDATE projektek SET " +
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
            MySql.update(command);
            int proID = Convert.ToInt16(MySql.MysqlReaderExecute_List("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            ProjektID = proID;
        }
        public void statusChange(int stat) // javított
        {
            if(projectSearchMemory != null)
            projectSearchMemory.Clear();
            projectSearchMemory.Add(new Projekt_Search_Memory() { statusz = stat });
        }
        public void projectDescriptionUpdate(string type, string content) // javított
        {
            string command = "";
            switch (type)
            {
                case "feladatok":
                    command = "UPDATE projektek SET feladatok='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = "+sess.UserData[0].id+"";
                    break;
                case "elvarasok":
                    command = "UPDATE projektek SET elvarasok='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = " + sess.UserData[0].id + "";
                    break;
                case "kinalunk":
                    command = "UPDATE projektek SET kinalunk='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = " + sess.UserData[0].id + "";
                    break;
                case "elonyok":
                    command = "UPDATE projektek SET elonyok='" + content + "' WHERE projektek.id = " + ProjektID + " AND hr_id = " + sess.UserData[0].id + "";
                    break;
                default:
                    break;
            }
            MySql.update(command);
        }
        public List<koltsegek> Data_ProjectCost()  // javított
        {
            string command = "SELECT * FROM projekt_koltsegek WHERE projekt_id = "+ProjektID+"";
            return MySql.Koltsegek_MySql_listQuery(command);
        }
        public void projectCostInsert(string megnevezes, string osszeg)  // javított
        {
            string command = "INSERT INTO `projekt_koltsegek` (id, projekt_id, koltseg_megnevezes, osszeg) VALUES (null, "+ProjektID+", '"+megnevezes+"', "+osszeg+");";
            MySql.update(command);
        }
        public void projectCostDelete(int id)  // javított
        {
            string command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.id = " + id + "";
            MySql.update(command);
        }
    }
}
