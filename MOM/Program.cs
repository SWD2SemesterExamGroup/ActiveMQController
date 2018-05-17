namespace MOM
{
    using MOM.ActiveMQ;
    using MOM.Helpers.ContentFilters;
    using MOM.WebServiceControllers;
    using Newtonsoft.Json.Linq;
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
            /*
            String text = "{'studentID':1,'key':'.o)l)XvYDg','accept':false}";
            JObject json = null;
            try
            {
                json = JObject.Parse(text);
            } catch (ArgumentException eA) {
                Debug.WriteLine(eA);
            }
            
            /* Test student key check */
            /*Console.WriteLine("Json Object: " + json.ToString());
            CourseAccessWS courseAccessWS = new CourseAccessWS();
            JObject success = courseAccessWS.checkKey((String)json["key"]);
            Console.WriteLine("Course Access WS: " + success.ToString());

            // Instatiate Service
            AttendanceLogWS attendanceLogWS = new AttendanceLogWS();
            // Modify json object to have one more field courseID
            json["courseid"] = success["CourseID"];
            json["success"] = success["success"];

            Console.WriteLine("Json Object Before Sending to ALogWS: " + json.ToString());
            attendanceLogWS.addToAttendanceLog(json);


            while (true) {  }
            */
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
