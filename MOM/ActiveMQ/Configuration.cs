namespace MOM.ActiveMQ
{
    using Apache.NMS;
    using System;

    /// <summary>
    /// Configuration class for the connection factory for online AWS broker
    /// </summary>
    public class Configuration
    {
        // Variables
        // AWS MQ Broker Connection
        private Uri connecturi = new Uri("activemq:ssl://b-27699194-d867-4b89-a04f-c448b445ae8d-1.mq.us-east-2.amazonaws.com:61617?wireFormat.tightEncodingEnabled=true");
        // User login information
        public const String USER = "MOM";
        public const String PASSWORD = "B3nderKlaede%";

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <value>
        /// The factory.
        /// </value>
        protected IConnectionFactory factory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            Console.WriteLine("About to connect to " + connecturi);

            // NOTE: ensure the nmsprovider-activemq.config file exists in the executable folder.
            factory = new NMSConnectionFactory(connecturi);
        }
    }
}
