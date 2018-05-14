using System;
using Apache.NMS;
using Apache.NMS.Util;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using MOM.KEA_Organization;
using System.Web.Services.Protocols;
using MOM.ActiveMQ;
using MOM.SandBox;

namespace MOM
{
    class Program
    {
        /**
         * Example of ActiveMQ Consumer
         * http://activemq.apache.org/nms/examples.html
         */
        public static void Main(string[] args)
        {
            /* Test call to PHP REST API */
            //CoursePasswordTasks test1 = new CoursePasswordTasks();
            //test1.callWSGet().Wait();
            /* Setup Consumers to AWS MQ */
            NMSConsumer teacherRoute = new NMSConsumer();
            teacherRoute.startReceivers();
        }
    }
}
