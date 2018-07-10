using HRCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.Net;
using System.DirectoryServices.Protocols;
using System.DirectoryServices.AccountManagement;

namespace HRCloud.Control
{
    class login_cont
    {
        Session session = new Session();
        dbEntities dbE = new dbEntities();
        public bool ActiveDirectoryValidation(string name, string pass)
        {
            bool authenticated = false;

            //try
            //{
            //    DirectoryEntry entry = new DirectoryEntry("ldap://192.168.144.21:389", name + "@pmhu.local", pass);
            //    object nativeObject = entry.NativeObject;
            //    authenticated = true;
            //}
            //catch (DirectoryServicesCOMException cex)
            //{
            //    //not authenticated; reason why is in cex
            //}
            //catch (Exception ex)
            //{
            //    //not authenticated due to some other exception [this is optional]
            //}

            //return authenticated;
            //try
            //{
            //    LdapConnection connection = new LdapConnection("ldap://192.168.144.21:389");
            //    NetworkCredential credential = new NetworkCredential(name + "@pmhu.local", pass);
            //    connection.Credential = credential;
            //    connection.Bind();
            //    Console.WriteLine("logged in");
            //    authenticated = true;
            //}
            //catch (LdapException lexc)
            //{
            //    String error = lexc.ServerErrorMessage;
            //    Console.WriteLine(lexc);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc);
            //}
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "ldap://192.168.144.21:389"))
                {
                    // validate the credentials
                    authenticated = pc.ValidateCredentials(name, pass);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return authenticated;
        }

        public bool userValidation(string name, string pass)
        {
            DateTime dateTime = DateTime.Now;
            bool valider = false;
                int found = dbE.MysqlReaderRowCount("SELECT count(id) FROM users WHERE username='" + name + "'");
                if (found == 1)
                {
                    valider = true;
                    dbE.SqliteQueryExecute("UPDATE users SET belepve = '" + dateTime.ToString("yyyy. MM. dd.") + "' WHERE username = '" + name + "';");
                }

            return valider;
        }
        public string GetRememberedUser()
        {
            string user = "";
            try
            {
                user = dbE.SqliteReaderExecute("select username from app");
            }
            catch (Exception)
            {
                dbE.SqliteQueryExecute("CREATE TABLE IF NOT EXISTS 'app' ('username' TEXT);");
                user = dbE.SqliteReaderExecute("SELECT 'username' FROM 'app';");
            }
            return user;
        }
        public void WriteRememberedUser(string username)
        {
            dbE.SqliteQueryExecute("DELETE FROM 'app';");
            dbE.SqliteQueryExecute("INSERT INTO 'app' (username) VALUES ('" + username + "');");
        }
        public void DeleteRememberedUser()
        {
            dbE.SqliteQueryExecute("DELETE FROM 'app';");
        }
        public bool UserValider_MySql(string user)
        {
            string query = "SELECT count(id) FROM users WHERE username='" + user + "'";
            return dbE.SimpleValider_MySQL(query);
        }
        public List<UserSessData> UserSessionDataList(string username)
        {
            string query = "SELECT * FROM users WHERE username='" + username + "'";

            List<UserSessData> list = dbE.UserSession(query);

            return list;
        }
        public void UserRegistration(string username, string name, string email, int kategoria)
        {
            DateTime dateTime = DateTime.Now;
            dbE.MysqlQueryExecute("INSERT INTO `users` (`id`, `username`, `name`, `email`, `kategoria`, `jogosultsag`, `validitas`, `belepve`, `reg_datum`) VALUES (NULL, '"+ username + "', '"+ name + "', '"+ email + "', '"+ kategoria + "', '1', '1', '" + dateTime.ToString("yyyy. MM. dd.") + "', '" + dateTime.ToString("yyyy. MM. dd.") + "');");
        }
    }
}
