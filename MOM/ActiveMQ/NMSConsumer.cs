namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using MOM.ActiveMQ.Teacher;
    using MOM.Helpers.ContentFilters;
    using MOM.WebServiceControllers;

    // Could become TeacherConsumer
    /// <summary>
    /// NMS Consumer also know as the teacher consumer
    /// </summary>
    /// <seealso cref="MOM.ActiveMQ.Configuration" />
    public class NMSConsumer : Configuration
    {
        // Variables
        private IDestination destination;
        private const String PATH_QUEUE = "queue://jsa-queue";
        private IDestination destinationCourse;
        private const String PATH_TOPIC_COURSE_ATTENDANCE = "queue://course-keys";

        /// <summary>
        /// Keys the course.
        /// </summary>
        public void keyCourse()
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
                            Console.WriteLine("Message Received!!: " + message.Text);
                            Debug.WriteLine("Message Received!!: \n" + message.Text);

                            // Php Service Call to insert newly generated passwords
                            // TODO: Interpert message, do content filtering and send to php WS
                            JObject teacherPHP = new PHPWSFilter().filterToWS(message.Text);

                            CourseAccessWS caws = new CourseAccessWS();

                            var item = caws.insertKey(teacherPHP);

                            Debug.WriteLine(item);

                            NMSProducer producer = new NMSProducer();
                            producer.keyResponse(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start()
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                // Instantiation of destination
                destination = SessionUtil.GetDestination(session, PATH_QUEUE);
                Console.WriteLine("Using destination: " + destination);

                // Create a consumer
                using (IMessageConsumer consumer = session.CreateConsumer(destination))
                {
                    connection.Start();
                    // Start the connection so that messages will be processed.
                    while (true)
                    { 
                        // Set up receiver for destination queue
                        ITextMessage message = consumer.Receive() as ITextMessage;

                        if (message == null)
                        {
                            Debug.WriteLine("No message received!");
                        }
                        else
                        {
                            /*
                            Console.WriteLine("Received message with ID:   " + message.NMSMessageId);
                            Console.WriteLine("Received message with Correlation ID:   " + message.NMSCorrelationID);
                            Console.WriteLine("Received message with Type:   " + message.NMSType);
                            Console.WriteLine("Received message with text: " + message.Text);
                            */
                            // parse Json to object
                            JObject jObject = JObject.Parse(message.Text);

                            // Add teacher id
                            int teacherID = int.Parse(jObject["teacherID"].ToString());

                            //Console.WriteLine("From Json to Object");
                            //Console.WriteLine("ID: " + teacherID);

                            // Using ws to retreive data from WS
                            Console.WriteLine("Calling KEA Organization WS");

                            // Initialize Teacher WS
                            WSConsumer wSConsumer = new WSConsumer();
                            
                            // get teacher data from KEA Organization web service
                            string jsonResult = wSConsumer.getTeacherEntityJsonBy(teacherID);
                            //Console.WriteLine("Json Result  : \n" + jsonResult);

                            // Instantiatet Producer
                            NMSProducer producerOwn = new NMSProducer();
                            // produce result to teacher client through ActiveMQ
                            producerOwn.start(jsonResult);
                        }
                    }
                }
            }
        }
    }
}