namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using System;

    public class Configuration
    {
        private Uri connecturi = new Uri("activemq:tcp://localhost:61616");
        protected IConnectionFactory factory { get; }

        public Configuration()
        {
            Console.WriteLine("About to connect to " + connecturi);

            // NOTE: ensure the nmsprovider-activemq.config file exists in the executable folder.
            factory = new NMSConnectionFactory(connecturi);
        }
    }
}
