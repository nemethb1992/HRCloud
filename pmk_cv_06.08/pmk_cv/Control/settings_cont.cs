using System;
using HRCloud.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HRCloud.Control
{
    class settings_cont
    {
        dbEntities dbE = new dbEntities();
        public List<ertesitendok_struct> Ertesitendok_DataSource()
        {
            string query = "SELECT * FROM users WHERE kategoria = 0";
            return dbE.Ertesitendok_MySql_listQuery(query);
        }
        public List<nyelv_struct> Nyelv_DataSource()
        {
            string query = "SELECT * FROM nyelv";
            return dbE.Nyelv_MySql_listQuery(query);
        }
        public List<munkakor_struct> Munkakor_DataSource()
        {
            string query = "SELECT * FROM munkakor";
            return dbE.Munkakor_MySql_listQuery(query);
        }
        public List<pc_struct> PC_DataSource()
        {
            string query = "SELECT * FROM pc";
            return dbE.PC_MySql_listQuery(query);
        }
        public List<vegzettseg_struct> Vegzettseg_DataSource()
        {
            string query = "SELECT * FROM vegzettsegek";
            return dbE.Vegzettseg_MySql_listQuery(query);
        }
        public List<ertesulesek> Ertesulesek_DataSource()
        {
            string query = "SELECT * FROM ertesulesek";
            return dbE.Ertesulesek_MySql_listQuery(query);
        }
        public void item_delete(int id, string table)
        {
            string query = "DELETE FROM "+table+" WHERE id="+id+"";
            dbE.MysqlQueryExecute(query);
        }
        public void item_insert(string content, string table)
        {
            string query = "";

            switch (table)
            {
                case "ertesitendok":
                    query = "INSERT INTO `ertesitendok` (`id`, `ertesitendok_nev`, `email`, `telefon`) VALUES (NULL, '" + content + "', 'email', '000');";
                    break;
                case "vegzettsegek":
                    query = "INSERT INTO `vegzettsegek` (`id`, `megnevezes_vegzettseg`) VALUES(NULL, '" + content + "')";
                    break;
                case "munkakor":
                    query = "INSERT INTO `munkakor` (`id`, `megnevezes_munka`) VALUES (NULL, '" + content + "');";
                    break;
                case "pc":
                    query = "INSERT INTO `pc` (`id`, `megnevezes_pc`) VALUES (NULL, '" + content + "');";
                    break;
                case "ertesulesek":
                    query = "INSERT INTO `ertesulesek` (`id`, `ertesules_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
                case "nyelv":
                    query = "INSERT INTO `nyelv` (`id`, `megnevezes_nyelv`) VALUES (NULL, '" + content + "');";
                    break;
                case "kompetenciak":
                    query = "INSERT INTO `kompetenciak` (`id`, `kompetencia_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
            }

            dbE.MysqlQueryExecute(query);
        }
        public void kompetenciaíró()
        {
            string kompressed = "proaktivitás;önállóság;együttműködő képesség;kommunikációs képesség;releváns szakmai tapasztalat;rendszerben való gondolkodás;jó problémamegoldó képesség;jó kommunikációs képesség;nyitottság;rugalmasság;konfliktus kezelés;terhelhetőség;pontosság;kommunikációs német nyelvtudás;kommunikációs angol nyelvtudás;minőségközpontú szemlélet;lojalitás;precíz munkavégzés;monotónia tűrés;hatékony időgazdálkodás;magabiztos fellépés;jó kézügyesség;jó állóképesség;tanulási, fejlődési hajlandóság;többműszakos munkarend vállalása; analitikus gondolkodás; önálló döntéshozás;műszaki gondolkodás;projekt szemlélet;gyakorlatias személyiség;önálló, precíz személyiség;dinamikus személyiség;csapatmunka;";
            string[] s = kompressed.Split(';');
            foreach (var item in s)
            {
                string query = "INSERT INTO `kompetenciak` (`id`, `kompetencia_megnevezes`) VALUES (NULL, '" + item + "');";
                dbE.MysqlQueryExecute(query);
            }
            MessageBox.Show("kész");
        }
    }
}
