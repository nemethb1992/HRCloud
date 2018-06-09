using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Control
{
    class megjegyzes_cs
    {
        applicant_cont acontrol = new applicant_cont();
        projekt_cont pcontrol = new projekt_cont();
        dbEntities dbE = new dbEntities();
        Session sess = new Session();
        public void megjegyzes_feltoltes(string megjegyzes, int projekt_id, int applicant_id, int teljesites)
        {
            DateTime dateTime = DateTime.Now;
            if (teljesites >= 10)
            {
                teljesites = 10;
            }
            string query = "INSERT INTO megjegyzesek (jelolt_id,projekt_id,hr_id,hr_nev,megjegyzes,datum,ertekeles) VALUES (" + applicant_id + "," + projekt_id + "," + sess.UserData[0].id + ",'" + sess.UserData[0].name + "','" + megjegyzes + "','" + dateTime.ToString("yyyy. MM. dd.") + "'," + teljesites + ")";
            dbE.MysqlQueryExecute(query);
        }
        public void megjegyzes_torles(int megjegyzes_id, int hr_id, int projekt_id, int jelolt_id)
        {
            string query = "DELETE FROM megjegyzesek WHERE megjegyzesek.id = "+megjegyzes_id+" AND hr_id = "+hr_id+" AND projekt_id = "+projekt_id+" AND jelolt_id = "+jelolt_id+"";
            dbE.MysqlQueryExecute(query);
        }
    }
}
