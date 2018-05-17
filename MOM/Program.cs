namespace MOM
{
    using MOM.ActiveMQ;
    using MOM.Helpers.ContentFilters;
    using MOM.WebServiceControllers;
    using System;
    using System.Diagnostics;

    class Program
    {
        /**
         * Example of ActiveMQ Consumer
         * http://activemq.apache.org/nms/examples.html
         */
        public static void Main(string[] args)
        {


            /* Test for filter of message text */
            //PHPWSFilter phpFilter = new PHPWSFilter();
            //phpFilter.filterToWS();
            /* Test of Course Access WS */
            //CourseAccessWS caws = new CourseAccessWS();
            //var item = caws.insertKey(phpFilter.filterToWS(""));
            //Debug.WriteLine("Response item: " + item);
            /* Test call to PHP REST API */
            //CoursePasswordTasks test1 = new CoursePasswordTasks();
            //test1.callWSGet().Wait();
            /* Setup Consumers to AWS MQ */
            NMSConsumer teacherRoute = new NMSConsumer();
            teacherRoute.startReceivers();
        }
    }
}
