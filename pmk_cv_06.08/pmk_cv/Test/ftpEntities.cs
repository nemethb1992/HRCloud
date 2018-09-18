using HRCloud.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Control
{
    class ftpEntities
    {
        //public static string ftpUser = "pmkcvtest";
        //public static string ftpPassword = "123456";
        
        //public bool ftpFileDownload(string name, string url, string filename, string extension)
        //{
        //    bool response = false;
        //    string path = "";
        //    try
        //    {
        //        FtpWebRequest request =
        //        (FtpWebRequest)WebRequest.Create("ftp://s7.nethely.hu/"+ url +"/"+ filename + "."+ extension);
        //        request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
        //        request.Method = WebRequestMethods.Ftp.DownloadFile;

        //        SaveFileDialog ofd = new SaveFileDialog();
        //        ofd.FileName = "HRcloud_" + name + "_cv." + extension;
        //        ofd.Filter = "All files (*.*)|*.*";
        //        if (ofd.ShowDialog() == true)
        //        {
        //            path = ofd.FileName;
        //        }

        //        using (Stream ftpStream = request.GetResponse().GetResponseStream())
        //        using (Stream fileStream = File.Create(path))
        //        {
        //            ftpStream.CopyTo(fileStream);
        //        }
        //        response = true;
        //    }
        //    catch (Exception e)
        //    {
        //        string err = e.ToString();
        //        response = false;
        //    }
        //    return response;
        //}


        //public List<ftp_feltoltes> ftpFileUpload(string path, string url, string id, string nev)
        //{
        //    string name = path.Substring(path.LastIndexOf(@"/") + 1);
        //    List<ftp_feltoltes> list = new List<ftp_feltoltes>();
        //    bool response = false;
        //    try
        //    {

        //        string extension = path.Split('.')[1];
        //        FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create("ftp://s7.nethely.hu/" + url + "/"+ name);
        //        ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);
        //        ftp.Method = WebRequestMethods.Ftp.UploadFile;

        //        ftp.KeepAlive = true;
        //        ftp.UseBinary = true;
        //        ftp.Method = WebRequestMethods.Ftp.UploadFile;


        //        FileStream fs = File.OpenRead(path);
        //        byte[] buffer = new byte[fs.Length];
        //        fs.Read(buffer, 0, buffer.Length);
        //        fs.Close();

        //        Stream ftpstream = ftp.GetRequestStream();
        //        ftpstream.Write(buffer, 0, buffer.Length);
        //        ftpstream.Close();
        //        response = true;

        //        list.Add(new ftp_feltoltes() { allapot = response, kiterjesztes = extension });
        //    }
        //    catch (Exception)
        //    {
        //        response = false;
        //    }
        //    return list;
        //}
    }
}
