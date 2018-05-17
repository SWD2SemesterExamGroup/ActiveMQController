namespace MOM.ActiveMQ.Student
{
    using Apache.NMS;
    using Apache.NMS.Util;
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

                            // TODO: Filter and deligate messages to diff webservices

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
