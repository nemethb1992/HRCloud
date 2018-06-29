using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ActiveUp.Net.Mail;

namespace HRCloud.Control
{
    class email_cont
    {
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
        public void ReadImap()
        {
            var mailRepository = new MailRepository(
                                    "imap.gmail.com",
                                    993,
                                    true,
                                    "nemethb1992@gmail.com",
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
    }
}
