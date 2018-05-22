namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// A common producer used for the teacher client app
    /// </summary>
    /// <seealso cref="MOM.ActiveMQ.Configuration" />
    public class NMSProducer : Configuration
    {
        // Variables
        private IDestination destination, destinationKeySuccess;
        private const String PATH_QUEUE = "queue://jsa-queue", PATH_KEY_SUCCESS = "queue://course-key-response";

        /// <summary>
        /// Starts the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void start(string text)
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                destination = SessionUtil.GetDestination(session, PATH_QUEUE);
                Console.WriteLine("Using destination: " + destination);

                // Create a consumer
                using (IMessageProducer producer = session.CreateProducer(destination))
                {
                    // Start the connection so that messages will be processed.
                    connection.Start();

                    producer.DeliveryMode = MsgDeliveryMode.Persistent;

                    ITextMessage request = session.CreateTextMessage(text);

                    //Console.WriteLine(request.Text);
                    Debug.WriteLine(request.Text);

                    producer.Send(request.Text);
                    Console.WriteLine("Message Send to ActiveMQ Queue");
                }
            }
        }

        /// <summary>
        /// Produce response to Keys insert from Teacher Client
        /// </summary>
        /// <param name="text">The text.</param>
        public void keyResponse(string text)
        {
            using (IConnection connection = factory.CreateConnection(USER, PASSWORD))
            using (ISession session = connection.CreateSession())
            {
                destinationKeySuccess = SessionUtil.GetDestination(session, PATH_KEY_SUCCESS);
                Console.WriteLine("Using destination: " + destinationKeySuccess);

                // Create producer
                using (IMessageProducer producer = session.CreateProducer(destinationKeySuccess))
                {
                    // Start connection
                    connection.Start();

                    producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

                    ITextMessage request = session.CreateTextMessage(text);

                    //Console.WriteLine(request.Text);
                    Debug.WriteLine(request.Text);

                    producer.Send(request.Text);
                    Console.WriteLine("Message Send!!\nMessage: " + text + "\nDestination: " + destinationKeySuccess);
                }
            }
        }
    }
}
