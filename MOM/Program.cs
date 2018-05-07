using System;
using Apache.NMS;
using Apache.NMS.Util;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using MOM.KEA_Organization;
using System.Web.Services.Protocols;
using MOM.ActiveMQ;

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
            NMSConsumer teacherRoute = new NMSConsumer();
            teacherRoute.start();
        }
    }
}
