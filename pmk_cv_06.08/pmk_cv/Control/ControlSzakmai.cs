using HRCloud.Model;
using System.Collections.Generic;
using static HRCloud.Model.ModelSzakmai;
using HRCloud.Source;

namespace HRCloud.Control
{
    class ControlSzakmai
    {
        Model.MySql mySql = new Model.MySql();
        Session session = new Session();

        public List<Projekt_Bevont_struct> Data_SzakmaiProject()
        {
            string command = "SELECT coalesce((SELECT count(jelolt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id),0) as jeloltek_db, projektek.id, megnevezes_projekt, megnevezes_munka FROM projektek INNER JOIN munkakor ON munkakor.id = projektek.munkakor INNER JOIN projekt_ertesitendok_kapcs ON projektek.id = projekt_ertesitendok_kapcs.projekt_id WHERE projektek.statusz = 1 AND ertesitendok_id = " + session.UserData[0].id + "";
            List<Projekt_Bevont_struct> list = mySql.getSzakmaiProject(command);
            mySql.close();
            return list;
        }

        public List<interju_struct> Data_SzakmaiInterview()
        {
            string command = "SELECT interjuk_kapcs.id, megnevezes_projekt, interjuk_kapcs.projekt_id, interjuk_kapcs.jelolt_id, jeloltek.nev,interju_datum,interju_cim,helyszin FROM interju_resztvevo_kapcs INNER JOIN interjuk_kapcs ON interju_resztvevo_kapcs.interju_id = interjuk_kapcs.id " +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE interju_resztvevo_kapcs.user_id = " + session.UserData[0].id + " ORDER BY interju_datum";
            List<interju_struct> list = mySql.getSzakmaiInterview(command);
            mySql.close();
            return list;
        }
    }
}
