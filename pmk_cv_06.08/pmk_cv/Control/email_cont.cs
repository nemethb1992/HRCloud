using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ActiveUp.Net.Mail;
using HRCloud.Model;
using static HRCloud.Model.Email_m;

namespace HRCloud.Control
{
    class email_cont
    {
        dbEntities dbE = new dbEntities();
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
        public void ReadImap()
        {
            List<MailServer_m> li = SMTP_List();
            var mailRepository = new MailRepository(
                                    li[0].mailserver,
                                    li[0].port,
                                    li[0].ssl,
                                    li[0].login,
                                    "3hgb8wy3hgb8wy"
                                );
            var emailList = mailRepository.GetUnreadMails("inbox");
            foreach (Message email in emailList)
            {
                if (email.From.Email.ToString() == "fzbalu92@gmail.com")
                {
                    MessageBox.Show("siker");
                }

            }
        }

        private void Mail_Send()
        {

            try
            {
                MailMessage mail = new MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("your_email_address@gmail.com");
                mail.To.Add("to_address");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
