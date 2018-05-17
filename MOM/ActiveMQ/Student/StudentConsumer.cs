namespace MOM.ActiveMQ.Student
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using MOM.WebServiceControllers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;

    public class StudentConsumer : Configuration
    {
        private IDestination studentDestiantion;
        private const String PATH_KEY_STUDENT = "queue://student-key";

        // Receiving messages from student trying out a key
        public void studentKey()
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                studentDestiantion = SessionUtil.GetDestination(session, PATH_KEY_STUDENT);
                Console.WriteLine("Using destination: " + studentDestiantion);

                // Create a consumer
                using (IMessageConsumer consumer = session.CreateConsumer(studentDestiantion))
                {
                    connection.Start();

                    /*
                     Console.WriteLine();
                     Debug.WriteLine();
                     */

                    while (true)
                    {
                        ITextMessage message = consumer.Receive() as ITextMessage;
                        if (message == null)
                        {
                            Debug.WriteLine("No message received!");
                        }
                        else
                        {
                            Console.WriteLine("Message Received!!: " + message.Text);
                            Debug.WriteLine("Message Received!!: \n" + message.Text);
                            // Add message ass json
                            JObject json = JObject.Parse(message.Text);

                            // New instance of Course Access Web Service
                            CourseAccessWS courseAccessWS = new CourseAccessWS();
                            // Send key for check and getting response
                            JObject success = courseAccessWS.checkKey((String)json["key"]);

                            // Instatiate Service
                            AttendanceLogWS attendanceLogWS = new AttendanceLogWS();
                            // Modify json object to have one more field courseID
                            json["courseid"] = success["CourseID"];
                            json["success"] = success["success"];
                            
                            var responseLog = attendanceLogWS.addToAttendanceLog(json);

                            StudentProducer producer = new StudentProducer();
                            JObject response = new JObject();
                            response.Add("success", (String)json["success"]);
                            producer.ResponseToKeyAttendance(response.ToString());
                            /*
                             * try key at php ws get all ids and a success message response
                             * call attendance log WS to post data
                             * Return success response to student client queue student-key
                             */
                        }
                    }
                }
            }
        }
    }
}
