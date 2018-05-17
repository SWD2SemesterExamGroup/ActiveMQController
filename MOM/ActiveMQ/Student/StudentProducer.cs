namespace MOM.ActiveMQ.Student
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Student Producer sends messages to the MQ Broker for the student functionality
    /// </summary>
    /// <seealso cref="MOM.ActiveMQ.Configuration" />
    public class StudentProducer : Configuration
    {
        private IDestination Destination;
        private const String PATH_QUEUE = "queue://student-key-response";

        public void ResponseToKeyAttendance(String message)
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                Destination = SessionUtil.GetDestination(session, PATH_QUEUE);
                Console.WriteLine("Using destination: " + Destination);

                // Create a consumer
                using (IMessageProducer producer = session.CreateProducer(Destination))
                {
                    // Start the connection so that messages will be processed.
                    connection.Start();

                    producer.DeliveryMode = MsgDeliveryMode.Persistent;

                    ITextMessage request = session.CreateTextMessage(message);

                    Console.WriteLine(request.Text);
                    Debug.WriteLine(request.Text);

                    producer.Send(request.Text);
                    Console.WriteLine("Send to ActiveMQ Queue\n" + Destination);
                }
            }
        }
    }
}
