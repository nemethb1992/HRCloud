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
using System.Windows;

namespace HRCloud.Control
{
    class ControlLogin
    {
        Session session = new Session();
        Model.MySql dbE = new Model.MySql();
        public bool ActiveDirectoryValidation(string username, string password)
        {
            bool authenticated = false;
            if (password.Length > 0)
            {
                try
                {
                    LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier("192.168.144.21", 389);
                    LdapConnection ldapConnection = new LdapConnection(ldi);
                    ldapConnection.AuthType = AuthType.Basic;
                    ldapConnection.SessionOptions.ProtocolVersion = 3;
                    NetworkCredential nc = new NetworkCredential(username + "@pmhu.local", password);
                    ldapConnection.Bind(nc);
                    ldapConnection.Dispose();
                    authenticated = true;
                }
                catch (LdapException e)
                {
                    Console.WriteLine("\r\nUnable to login:\r\n\t" + e.Message);
                    authenticated = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\r\nUnexpected exception occured:\r\n\t" + e.GetType() + ":" + e.Message);
                    authenticated = false;
                }
            }
            return authenticated;
        }

        public bool userValidation(string name, string pass)
        {
            DateTime dateTime = DateTime.Now;
            bool valider = false;
                int found = dbE.rowCount("SELECT count(id) FROM users WHERE username='" + name + "'");
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
            return dbE.bind(query);
        }
        public List<UserSessData> UserSessionDataList(string username)
        {
            string query = "SELECT * FROM users WHERE username='" + username + "'";

            List<UserSessData> list = dbE.setUserSession(query);

            return list;
        }
        public void UserRegistration(string username, string name, string email, int kategoria)
        {
            DateTime dateTime = DateTime.Now;
            dbE.update("INSERT INTO `users` (`id`, `username`, `name`, `email`, `kategoria`, `jogosultsag`, `validitas`, `belepve`, `reg_datum`) VALUES (NULL, '"+ username + "', '"+ name + "', '"+ email + "', '"+ kategoria + "', '1', '1', '" + dateTime.ToString("yyyy. MM. dd.") + "', '" + dateTime.ToString("yyyy. MM. dd.") + "');");
        }
    }
}
