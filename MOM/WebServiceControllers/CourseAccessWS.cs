namespace MOM.WebServiceControllers
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Net;
    using System.Text;
    
    // TODO: Create connection to Course Access WS
    // TODO: Create methods to support endpoint
    public class CourseAccessWS
    {
        private const String BASE_PATH = "http://localhost";
        private const String BASE_PORT = ":9090";
        private const String BASE_DIRECTION = "/CourseAccessAPI/src";
        private const String BASE_FILE = "/api.php";
        private const String BASE_POST = "/post";
        private String destination;

        // https://msdn.microsoft.com/en-us/library/900ted1f(v=vs.110).aspx
        public String insertKey(JObject data)
        {
            destination = BASE_PATH + BASE_PORT + BASE_DIRECTION + BASE_FILE + BASE_POST;
            WebClient client = new WebClient();
            Debug.WriteLine("Destination: " + destination);
            Debug.WriteLine("Data: " + data.ToString());
            String responseValues = "";

            // New try
            ICollection<Byte[]> responses = new HashSet<Byte[]>();
            foreach (JObject key in data["keys"])
            {
                Debug.WriteLine(key);
                if (((String)key["key"]).Equals("BREAK")) {
                    continue;
                }
                NameValueCollection nameValueCollection = new NameValueCollection();

                nameValueCollection.Add("teacherid", (String)data["teacherid"]);
                nameValueCollection.Add("courseid", (String)data["courseid"]);
                nameValueCollection.Add("classid", (String)data["classid"]);

                nameValueCollection.Add("password", (String)key["key"]);
                nameValueCollection.Add("startdate", (String)key["startPoint"]["date"] + " " + (String)key["startPoint"]["timeDisplay"]);
                nameValueCollection.Add("expiredate", (String)key["expirationPoint"]["date"] + " " + (String)key["expirationPoint"]["timeDisplay"]);

                Debug.WriteLine("names and values: " + nameValueCollection);
                byte[] response = client.UploadValues(destination, nameValueCollection);
                responses.Add(response);

                Debug.WriteLine("reponse" + Encoding.ASCII.GetString(response));
            }

            foreach (var res in responses)
            {
                responseValues += Encoding.ASCII.GetString(res) + "\n";
            }
            Debug.WriteLine("All responses: " + responseValues);
            return responseValues;
        }
    }
}
