﻿using HRCloud.Model;
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
        Model.MySql mySql = new Model.MySql();
        SqLite sqLite = new SqLite();
        public bool ActiveDirectoryValidation(string username, string password)
        {
            if (password.Length > 0)
            {
                try
                {
                    LdapDirectoryIdentifier ldi = new LdapDirectoryIdentifier("192.168.144.21", 389);
                    LdapConnection ldapConnection = new LdapConnection(ldi);
                    ldapConnection.AuthType = AuthType.Basic;
                    ldapConnection.SessionOptions.ProtocolVersion = 3;
                    NetworkCredential networkCredential = new NetworkCredential(username + "@pmhu.local", password);
                    ldapConnection.Bind(networkCredential);
                    ldapConnection.Dispose();
                    return true;
                }
                catch (LdapException e)
                {
                    Console.WriteLine("\r\nUnable to login:\r\n\t" + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\r\nUnexpected exception occured:\r\n\t" + e.GetType() + ":" + e.Message);
                }
            }
            return false;
        }

        public bool userValidation(string name, string pass)
        {
            DateTime dateTime = DateTime.Now;
            if (mySql.rowCount("SELECT count(id) FROM users WHERE username='" + name + "'") == 1)
            {
                sqLite.update("UPDATE users SET belepve = '" + dateTime.ToString("yyyy. MM. dd.") + "' WHERE username = '" + name + "';");
                return true;
            }
            return false;
        }
        public string getRememberedUser()
        {
            string user;
            try
            {
                user = sqLite.query("select username from app");
            }
            catch (Exception)
            {
                sqLite.update("CREATE TABLE IF NOT EXISTS 'app' ('username' TEXT);");
                user = sqLite.query("SELECT 'username' FROM 'app';");
            }
            return user;
        }
        public void writeRememberedUser(string username) //javítva használja: login
        {
            sqLite.update("DELETE FROM 'app';");
            sqLite.update("INSERT INTO 'app' (username) VALUES ('" + username + "');");
        }
        public void deleteRememberedUser() //javítva használja: login
        {
            sqLite.update("DELETE FROM 'app';");
        }
        public bool mySqlUserValidation(string user) //javítva használja: login
        {
            return mySql.bind("SELECT count(id) FROM users WHERE username='" + user + "'");
        }
        public List<UserSessData> Data_UserSession(string username)  //javítva használja: login
        {
            return mySql.getUserSession("SELECT * FROM users WHERE username='" + username + "'");
        }
        public void userRegistration(string username, string name, string email, int kategoria)
        {
            DateTime dateTime = DateTime.Now;
            mySql.update("INSERT INTO `users` (`id`, `username`, `name`, `email`, `kategoria`, `jogosultsag`, `validitas`, `belepve`, `reg_datum`) VALUES (NULL, '"+ username + "', '"+ name + "', '"+ email + "', '"+ kategoria + "', '1', '1', '" + dateTime.ToString("yyyy. MM. dd.") + "', '" + dateTime.ToString("yyyy. MM. dd.") + "');");
        }
    }
}
