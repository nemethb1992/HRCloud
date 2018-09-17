using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Control
{
    class Comment
    {
        Model.MySql dbE = new Model.MySql();
        Session sess = new Session();
        public void megjegyzes_feltoltes(string megjegyzes, int projekt_id, int applicant_id, int teljesites)
        {
            DateTime dateTime = DateTime.Now;
            if (teljesites >= 10)
            {
                teljesites = 10;
            }
            string command = "INSERT INTO megjegyzesek (jelolt_id,projekt_id,hr_id,hr_nev,megjegyzes,datum,ertekeles) VALUES (" + applicant_id + "," + projekt_id + "," + sess.UserData[0].id + ",'" + sess.UserData[0].name + "','" + megjegyzes + "','" + dateTime.ToString("yyyy. MM. dd.") + "'," + teljesites + ")";
            dbE.update(command);
        }
        public void delete(int megjegyzes_id, int hr_id, int projekt_id, int jelolt_id)
        {
            string command = "DELETE FROM megjegyzesek WHERE megjegyzesek.id = "+megjegyzes_id+" AND hr_id = "+hr_id+" AND projekt_id = "+projekt_id+" AND jelolt_id = "+jelolt_id+"";
            dbE.update(command);
        }
    }
}
