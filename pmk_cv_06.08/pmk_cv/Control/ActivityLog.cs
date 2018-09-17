using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Control
{
    class ActivityLog
    {
        Model.MySql dbE = new Model.MySql();
        public void Activity_write()
        { 
            //List<activity_struct> list)
        
            DateTime dateTime = DateTime.Now;
            string query = "INSERT INTO `activity_log` (`id`, `hr_id`, `nev`, `tipus`, `leiras`, `datum`) VALUES(NULL, 1, 'nev', 'tipus', 'leiras', '" + dateTime.ToString() + "')";
            //string query = "INSERT INTO `activity_log` (`id`, `hr_id`, `nev`, `tipus`, `leiras`, `datum`) VALUES(NULL, "+list[1]+ ", '" + list[2] + "', '" + list[3] + "', '" + list[4] + "', '"+ dateTime.ToString("hu-HU") + "')";
            dbE.update(query);
        }
    }
}
