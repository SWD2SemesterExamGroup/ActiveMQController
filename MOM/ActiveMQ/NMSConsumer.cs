namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using MOM.KEA_Organization;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using MOM.ActiveMQ.Teacher;
    using Newtonsoft.Json;
    using System.Threading;

    // Could become TeacherConsumer
    public class NMSConsumer : Configuration
    {
        private IDestination destination;
        private const String PATH_QUEUE = "queue://jsa-queue";
        private IDestination destinationCourse;
        private const String PATH_TOPIC_COURSE_ATTENDANCE = "queue://course-keys";
        
        public void startReceivers()
        {
            Thread teacherThread1 = new Thread(new ThreadStart(new NMSConsumer().start));
            Thread teacherThread2 = new Thread(new ThreadStart(new NMSConsumer().courseAttendance));
            //courseAttendance();

            teacherThread1.Start();
            teacherThread2.Start();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("-------------MOM----------------");
            Console.WriteLine("  Mom's Friendly Robot Company  ");
            Console.WriteLine("-------------MOM----------------");
            Console.WriteLine("--------------------------------");

        }

        public void courseAttendance()
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                destinationCourse = SessionUtil.GetDestination(session, PATH_TOPIC_COURSE_ATTENDANCE);
                Console.WriteLine("Using destination: " + destinationCourse);

                // Create a consumer
                using (IMessageConsumer consumer = session.CreateConsumer(destinationCourse))
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
                            Console.WriteLine("Message Received!!: " + message);
                            Debug.WriteLine("Message Received!!: " + message);

                            // TODO: Interpert message and send to php WS
                        }
                    }
                }
            }
        }

        public void start()
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                destination = SessionUtil.GetDestination(session, PATH_QUEUE);
                Console.WriteLine("Using destination: " + destination);

                // Create a consumer
                using (IMessageConsumer consumer = session.CreateConsumer(destination))
                {
                    connection.Start();
                    // Start the connection so that messages will be processed.
                    while (true)
                    { 
                        ITextMessage message = consumer.Receive() as ITextMessage;

                        if (message == null)
                        {
                            Debug.WriteLine("No message received!");
                        }
                        else
                        {
                            Console.WriteLine("Received message with ID:   " + message.NMSMessageId);
                            Console.WriteLine("Received message with Correlation ID:   " + message.NMSCorrelationID);
                            Console.WriteLine("Received message with Type:   " + message.NMSType);
                            Console.WriteLine("Received message with text: " + message.Text);

                            // parse Json to object
                            JObject jObject = JObject.Parse(message.Text);

                            int teacherID = int.Parse(jObject["teacherID"].ToString());

                            Console.WriteLine("From Json to Object");
                            Console.WriteLine("ID: " + teacherID);

                            // Using ws to retreive data from WS
                            Console.WriteLine("Calling KEA Organization WS");

                            // Initialize Teacher WS
                            WSConsumer wSConsumer = new WSConsumer();
                            Console.WriteLine("Teacher as json string");
                            string jsonResult = wSConsumer.getTeacherEntityJsonBy(teacherID);
                            Console.WriteLine("Json Result  : \n" + jsonResult);

                            NMSProducer producerOwn = new NMSProducer();
                            producerOwn.start(jsonResult);
                        }
                    }
                }
            }
        }
    }
}