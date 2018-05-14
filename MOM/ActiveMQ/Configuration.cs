namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using System;

    public class Configuration
    {
        // Localhost
        //private Uri connecturi = new Uri("activemq:tcp://localhost:61616");
        // AWS MQ Broker Connection
        private Uri connecturi = new Uri("activemq:ssl://b-27699194-d867-4b89-a04f-c448b445ae8d-1.mq.us-east-2.amazonaws.com:61617?wireFormat.tightEncodingEnabled=true");
        public const String USER = "MOM";
        public const String PASSWORD = "B3nderKlaede%";

        protected IConnectionFactory factory { get; }

        public Configuration()
        {
            Console.WriteLine("About to connect to " + connecturi);

            // NOTE: ensure the nmsprovider-activemq.config file exists in the executable folder.
            factory = new NMSConnectionFactory(connecturi);
        }
    }
}
