using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRCloud.Model.Email_m;

namespace HRCloud.Control
{
    class file_cont
    {
        dbEntities dbE = new dbEntities();

        public List<Jelolt_File_Struct> Applicant_FolderReadOut(int ApplicantID)
        {
            DirectoryInfo directory;
            List<Jelolt_File_Struct> list = new List<Jelolt_File_Struct>();
            FileInfo[] articles;

            try
            {
                directory = new DirectoryInfo(ROOTurl()[0].url + ApplicantID);
                articles = directory.GetFiles("*.pdf");
                foreach (FileInfo file in articles)
                {
                    list.Add(new Jelolt_File_Struct { fajlnev = file.Name.Split('.')[0], path = file.FullName });
                }
            }
            catch (Exception)
            {
            }
            return list;
        }
        public List<file_url> ROOTurl()
        {
            string query = "SELECT * FROM ROOTurl";
            return dbE.file_url_ROOT_MySql_listQuery(query);
        }
        public void Applicant_Folder_Structure_Creator()
        {
            string query = "SELECT * FROM `jeloltek` GROUP BY email";
            List<SubJelolt> list = dbE.Jelolt_Short_MySql_listQuery(query);
            foreach (var item in list)
            {
                Directory.CreateDirectory(ROOTurl()[0].url + item.id);
            }
            
        }
        public void Projekt_Folder_Structure_Creator()
        {
            string query = "SELECT * FROM `projektek`";
            List<SubProjekt> list = dbE.Sub_Projekt_MySql_listQuery(query);
            foreach (var item in list)
            {
                Directory.CreateDirectory(ROOTurl()[0].url + item.id);
            }

        }
    }
}
