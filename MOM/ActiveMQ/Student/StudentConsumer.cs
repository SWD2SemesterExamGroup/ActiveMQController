namespace MOM.ActiveMQ.Student
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using MOM.WebServiceControllers;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The Student Consumer of the Amazon Web Service ActiveMQ
    /// </summary>
    /// <seealso cref="MOM.ActiveMQ.Configuration" />
    public class StudentConsumer : Configuration
    {
        // Variables
        private IDestination studentDestiantion;
        private const String PATH_KEY_STUDENT = "queue://student-key";

        /// <summary>
        /// Receiving messages from student trying out a key
        /// </summary>
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
                            
                            // Instantiate Producer
                            StudentProducer producer = new StudentProducer();
                            
                            // Instatiate Services
                            AttendanceLogWS attendanceLogWS = new AttendanceLogWS();
                            CourseAccessWS courseAccessWS = new CourseAccessWS();
                            // Send key for check and getting response
                            JObject success = courseAccessWS.checkKey((String)json["key"]);
                            
                            // Modify json object to have one more field courseID
                            json["courseid"] = success["CourseID"];
                            json["success"] = success["success"];
                            var responseLog = attendanceLogWS.addToAttendanceLog(json);
                            // Instantiate response object
                            JObject response = new JObject();
                            response.Add("success", (String)json["success"]); // Add value
                            // Produce and send Object
                            producer.ResponseToKeyAttendance(response.ToString());
                        }
                    }
                }
            }
        }
    }
}
