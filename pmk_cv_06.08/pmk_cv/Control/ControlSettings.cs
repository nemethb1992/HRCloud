using HRCloud.Model;
using System.Collections.Generic;

namespace HRCloud.Control
{
    class ControlSettings
    {
        Model.MySql mySql = new Model.MySql();

        public List<ertesitendok_struct> Data_Ertesitendok()
        {
            string command = "SELECT * FROM users WHERE kategoria = 0";
            List <ertesitendok_struct> list = mySql.getErtesitendok(command);
            mySql.close();
            return list;
        }

        public List<nyelv_struct> Data_Nyelv()
        {
            string query = "SELECT * FROM nyelv";
            List <nyelv_struct> list = mySql.getNyelv(query);
            mySql.close();
            return list;
        }

        public List<munkakor_struct> Data_Munkakorok()
        {
            string command = "SELECT * FROM munkakor";
            List <munkakor_struct> list = mySql.getMunkakorok(command);
            mySql.close();
            return list;
        }

        public List<pc_struct> Data_Pc()
        {
            string command = "SELECT * FROM pc";
            List <pc_struct> list = mySql.getPc(command);
            mySql.close();
            return list;
        }

        public List<vegzettseg_struct> Data_Vegzettseg()
        {
            string query = "SELECT * FROM vegzettsegek";
            List <vegzettseg_struct> list = mySql.Vegzettseg_MySql_listQuery(query);
            mySql.close();
            return list;
        }

        public List<ertesulesek> Data_Ertesulesek()
        {
            string command = "SELECT * FROM ertesulesek";
            List <ertesulesek> list = mySql.Ertesulesek_MySql_listQuery(command);
            mySql.close();
            return list;
        }

        public void settingDelete(int id, string table)
        {
            string command = "DELETE FROM "+table+" WHERE id="+id+"";
            mySql.update(command);
            mySql.close();
        }

        public void settingInsert(string content, string table)
        {
            string command = "";

            switch (table)
            {
                case "ertesitendok":
                    command = "INSERT INTO `ertesitendok` (`id`, `ertesitendok_nev`, `email`, `telefon`) VALUES (NULL, '" + content + "', 'email', '000');";
                    break;
                case "vegzettsegek":
                    command = "INSERT INTO `vegzettsegek` (`id`, `megnevezes_vegzettseg`) VALUES(NULL, '" + content + "')";
                    break;
                case "munkakor":
                    command = "INSERT INTO `munkakor` (`id`, `megnevezes_munka`) VALUES (NULL, '" + content + "');";
                    break;
                case "pc":
                    command = "INSERT INTO `pc` (`id`, `megnevezes_pc`) VALUES (NULL, '" + content + "');";
                    break;
                case "ertesulesek":
                    command = "INSERT INTO `ertesulesek` (`id`, `ertesules_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
                case "nyelv":
                    command = "INSERT INTO `nyelv` (`id`, `megnevezes_nyelv`) VALUES (NULL, '" + content + "');";
                    break;
                case "kompetenciak":
                    command = "INSERT INTO `kompetenciak` (`id`, `kompetencia_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
            }
            mySql.update(command);
            mySql.close();
        }
        //public void kompetenciaíró()
        //{
        //    string kompressed = "proaktivitás;önállóság;együttműködő képesség;kommunikációs képesség;releváns szakmai tapasztalat;rendszerben való gondolkodás;jó problémamegoldó képesség;jó kommunikációs képesség;nyitottság;rugalmasság;konfliktus kezelés;terhelhetőség;pontosság;kommunikációs német nyelvtudás;kommunikációs angol nyelvtudás;minőségközpontú szemlélet;lojalitás;precíz munkavégzés;monotónia tűrés;hatékony időgazdálkodás;magabiztos fellépés;jó kézügyesség;jó állóképesség;tanulási, fejlődési hajlandóság;többműszakos munkarend vállalása; analitikus gondolkodás; önálló döntéshozás;műszaki gondolkodás;projekt szemlélet;gyakorlatias személyiség;önálló, precíz személyiség;dinamikus személyiség;csapatmunka;";
        //    string[] s = kompressed.Split(';');
        //    foreach (var item in s)
        //    {
        //        string command = "INSERT INTO `kompetenciak` (`id`, `kompetencia_megnevezes`) VALUES (NULL, '" + item + "');";
        //        mySql.update(command);
        //    }
        //    MessageBox.Show("kész");
        //}
    }
}
