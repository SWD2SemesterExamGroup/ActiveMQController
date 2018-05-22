namespace MOM.WebServiceControllers
{
    using MOM.SandBox.HealthCheckAPI;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Connection and functionality of tje Course Access Web Service
    /// </summary>
    public class CourseAccessWS : AHealthCheck
    {
        // Variables
        private const String BASE_PATH = "http://localhost";
        private const String BASE_PORT = ":9090";
        private const String BASE_DIRECTION = "/CourseAccessAPI/src";
        private const String BASE_FILE = "/api.php";
        private String BASE_POST = "/post";
        private String BASE_HEALTH = "/health";
        private const String BASE_KEY_CHECK = "/keycheck";
        private String destination;

        public CourseAccessWS() : base("CourseAccessWS")
        {
        }

        /// <summary>
        /// Inserts the key.
        /// https://msdn.microsoft.com/en-us/library/900ted1f(v=vs.110).aspx
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>String from JObject data</returns>
        public String insertKey(JObject data)
        {
            // Setup destination path
            destination = BASE_PATH + BASE_PORT + BASE_DIRECTION + BASE_FILE + BASE_POST;
            // Instantiate Web Client
            WebClient client = new WebClient();
            // Debug
            Debug.WriteLine("Destination: " + destination);
            Debug.WriteLine("Data: " + data.ToString());
            // Instantiate responseValues
            String responseValues = "";

            // Byte Collection of responses
            ICollection<Byte[]> responses = new HashSet<Byte[]>();
            // For every key in JObject
            foreach (JObject key in data["keys"])
            {
                NameValueCollection nameValueCollection = new NameValueCollection();
                Debug.WriteLine(key);
                // Filter out breaks
                if (((String)key["key"]).Equals("BREAK")) {
                    continue;
                }
                
                // Add url encoded values
                nameValueCollection.Add("teacherid", (String)data["teacherid"]);
                nameValueCollection.Add("courseid", (String)data["courseid"]);
                nameValueCollection.Add("classid", (String)data["classid"]);

                nameValueCollection.Add("password", (String)key["key"]);
                nameValueCollection.Add("startdate", (String)key["startPoint"]["date"] + " " + (String)key["startPoint"]["timeDisplay"]);
                nameValueCollection.Add("expiredate", (String)key["expirationPoint"]["date"] + " " + (String)key["expirationPoint"]["timeDisplay"]);

                //Debug.WriteLine("names and values: " + nameValueCollection);
                // Get response from post to destination
                byte[] response = client.UploadValues(destination, nameValueCollection);
                // Add response
                responses.Add(response);

                Debug.WriteLine("reponse" + Encoding.ASCII.GetString(response));
            }

            // Encoded byte[] responses decoded as Strings
            foreach (var res in responses)
            {
                responseValues += Encoding.ASCII.GetString(res) + "\n";
            }
            Debug.WriteLine("All responses: " + responseValues);
            // Return responses
            return responseValues;
        }

        /// <summary>
        /// Checks the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>JObject</returns>
        public JObject checkKey(String key)
        {
            // Setup destination
            destination = BASE_PATH + BASE_PORT + BASE_DIRECTION + BASE_FILE + BASE_KEY_CHECK + "/" + key;
            // Client
            WebClient client = new WebClient();
            // Empty name value colltion
            NameValueCollection nvCollection = new NameValueCollection();

            // Respons
            byte[] response = client.UploadValues(destination, "POST", nvCollection);

            // Return object
            return JObject.Parse(Encoding.ASCII.GetString(response));
        }

        public override string CheckHealth()
        {
            destination = BASE_PATH + BASE_PORT + BASE_DIRECTION + BASE_FILE + BASE_HEALTH;
            WebClient client = new WebClient();
            Debug.WriteLine("Destination: " + destination);
            String response = "";
            try
            {
                // Creating stream to endpoint
                Stream stream = client.OpenRead(destination);
                StreamReader reader = new StreamReader(stream);
                response = reader.ReadToEnd();
            } catch (WebException eW)
            {
                response = "{'success':false}";
            }
            Debug.WriteLine("Stream Response: " + response);
            return response;
        }
    }
}