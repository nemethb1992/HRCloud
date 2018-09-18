using HRCloud.Control;
using HRCloud.View.Usercontrol.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRCloud.Test
{
    public class ThreadTest
    {
        public Thread thread;
        ApplicantList applicantList = new ApplicantList();
        public ThreadTest()
        {

        }

        int asd = 1;

        public void ASYNC_applicantListLoader()
        {
            thread = new Thread(applicantList.applicantListLoader);
            try
            {
                if(!thread.IsAlive)
                {
                    thread.Start();
                }
            }
            catch
            {

            }
        }

    }
}
