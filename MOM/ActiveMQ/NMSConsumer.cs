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

    // Could become TeacherConsumer
    public class NMSConsumer : Configuration
    {
        private IDestination destination;
        private const String PATH_QUEUE = "queue://jsa-queue";

        public void start()
        {
            using (IConnection connection = factory.CreateConnection())
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

                            /*teacherEntityView teacherEntity = wSConsumer.getTeacherEntityBy(teacherID);
                            //teacherEntityView teacherEntity = new teacherEntityView();
                            Console.WriteLine("Teacher entity view model to string");
                            Console.WriteLine(teacherEntity.ToString());

                            try {
                                JObject jWS = JObject.FromObject(teacherEntity);
                                Console.WriteLine("JObject jWS Log");
                                Console.WriteLine(jWS.ToString());

                                //NMSProducer producer = new NMSProducer();
                                //producer.start(jWS, session, connection);
                            } catch (JsonReaderException eJR)
                            {
                                Console.WriteLine("JObject error");
                                Console.WriteLine(eJR.ToString());
                            }*/
                            /*
                            using (IMessageProducer producer = session.CreateProducer()) {
                                try
                                {
                                    connection.Start();
                                    producer.DeliveryMode = MsgDeliveryMode.Persistent;
                                    ITextMessage response = producer.CreateTextMessage(jsonResult);
                                    producer.Send(response);
                                } catch (NotSupportedException eNS)
                                {
                                    Console.WriteLine("Producer Bug: \n" + eNS.ToString());
                                }
                            }*/
                            NMSProducer producerOwn = new NMSProducer();
                            producerOwn.start(jsonResult);
                        }
                    }
                }
            }
        }
    }
}
