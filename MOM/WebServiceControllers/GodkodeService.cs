namespace MOM.WebServiceControllers
{
    using MOM.SandBox.HealthCheckAPI;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    public class GodkodeService : AHealthCheck
    {
        // Connection Variables
        private const String BASE_PATH = "https://godkode.herokuapp.com";
        
        private const String BASE_GET_DATA = "/getdata";
        
        private String destination;

        public GodkodeService() : base("Legacy Service")
        {
        }
        public override string CheckHealth()
        {
            destination = BASE_PATH + BASE_GET_DATA;
            WebClient client = new WebClient();
            Debug.WriteLine("Destination: " + destination);


            String response = "";
            try
            {
                // Creating stream to endpoint
                Stream stream = client.OpenRead(destination);
                StreamReader reader = new StreamReader(stream);
                response = reader.ReadToEnd();
            }
            catch (WebException eW)
            {
                response = "{'success':false}";
            }
            Debug.WriteLine("Stream Response: " + response);

            return response;
        }
    }
}
