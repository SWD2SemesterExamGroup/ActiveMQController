namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using Apache.NMS.Util;
    using MOM.KEA_Organization;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using System.Web.Services.Protocols;

    public class NMSProducer : Configuration
    {
        private IDestination destination;
        private const String PATH_QUEUE = "queue://jsa-queue";

        public void start(JObject jWS, ISession session, IConnection connection)
        {
            using (session = connection.CreateSession())
            {
                destination = SessionUtil.GetDestination(session, PATH_QUEUE);
                Console.WriteLine("Using destination: " + destination);

                // Create a consumer
                using (IMessageProducer producer = session.CreateProducer(destination))
                {
                    // Start the connection so that messages will be processed.
                    connection.Start();
                    
                    producer.DeliveryMode = MsgDeliveryMode.Persistent;

                    // Send a message
                    //IMessageProducer prod = session.CreateProducer();
                    //prod.DisableMessageID = false;
                    //prod.Send(destination, prod.CreateTextMessage(jWS.ToString()));

                    ITextMessage request = session.CreateTextMessage(jWS.ToString());
                    
                    Console.WriteLine(request.Text);
                    Debug.WriteLine(request.Text);

                    producer.Send(request);
                    Console.WriteLine("Message Send to ActiveMQ Queue");
                }
            }
        }

        public void start(string text)
        {
            using (IConnection connection = factory.CreateConnection(Configuration.USER, Configuration.PASSWORD))
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

                    // Send a message
                    //IMessageProducer prod = session.CreateProducer();
                    //prod.DisableMessageID = false;
                    //prod.Send(destination, prod.CreateTextMessage(jWS.ToString()));

                    ITextMessage request = session.CreateTextMessage(text);

                    Console.WriteLine(request.Text);
                    Debug.WriteLine(request.Text);

                    producer.Send(request.Text);
                    Console.WriteLine("Message Send to ActiveMQ Queue");
                }
            }
        }
    }
}
