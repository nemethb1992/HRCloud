using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ActiveUp.Net.Mail;
using HRCloud.Model;
using static HRCloud.Model.Email_m;
using HRCloud.Public.templates;

namespace HRCloud.Control
{
    class email_cont
    {
        dbEntities dbE = new dbEntities();
        email_template e_temp = new email_template();
        public class MailRepository
        {
            private Imap4Client client;

            public MailRepository(string mailServer, int port, bool ssl, string login, string password)
            {
                if (ssl)
                    Client.ConnectSsl(mailServer, port);
                else
                    Client.Connect(mailServer, port);
                Client.Login(login, password);
            }

            public IEnumerable<Message> GetAllMails(string mailBox)
            {
                return GetMails(mailBox, "ALL").Cast<Message>();
            }

            public IEnumerable<Message> GetUnreadMails(string mailBox)
            {
                return GetMails(mailBox, "UNSEEN").Cast<Message>();
            }

            protected Imap4Client Client
            {
                get { return client ?? (client = new Imap4Client()); }
            }

            private MessageCollection GetMails(string mailBox, string searchPhrase)
            {
                Mailbox mails = Client.SelectMailbox(mailBox);
                MessageCollection messages = mails.SearchParse(searchPhrase);
                return messages;
            }
        }
        public List<MailServer_m> SMTP_List()
        {
            string query = "SELECT * FROM ConnectionSMTP";
            return dbE.ConnectionSMTP_DataSource(query);
        }
        //public void ReadImap()
        //{
        //    List<MailServer_m> li = SMTP_List();
        //    var mailRepository = new MailRepository(
        //                            li[0].mailserver,
        //                            li[0].port,
        //                            li[0].ssl,
        //                            li[0].login,
        //                            "3hgb8wy3hgb8wy"
        //                        );
        //    var emailList = mailRepository.GetUnreadMails("inbox");
        //    foreach (Message email in emailList)
        //    {
        //        if (email.From.Email.ToString() == "fzbalu92@gmail.com")
        //        {
        //            MessageBox.Show("siker");
        //        }

        //    }
        //}

        public void Mail_Send(string to, string email_body)
        {

            //var fromAddress = new MailAddress("nemethb1992@gmail.com");
            //var fromPassword = "3hgb8wy3hgb8wy";
            //var toAddress = new MailAddress(to);
            

            //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            //};

            //using (var message = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = "Phoenix Mecano Kecskemét kft (HR Cloud)",
            //    Body = email_body,
            //    IsBodyHtml = true

                
            //})
            //{
            //    smtp.Send(message);
            //}


            try
            {
                MailMessage mail = new MailMessage();

                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("nemethb1992@gmail.com", "3hgb8wy3hgb8wy");
                SmtpServer.EnableSsl = true;

                mail.From = new MailAddress("nemethb1992@gmail.com");
                mail.To.Add(to);
                mail.Subject = "Phoenix Mecano Kecskemét kft (HR Cloud)";
                mail.Body = email_body;
                mail.IsBodyHtml = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
