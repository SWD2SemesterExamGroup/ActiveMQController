using Newtonsoft.Json.Linq;
namespace MOM.WebServiceControllers
{
    using MOM.SandBox.HealthCheckAPI;
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The connection to the Attendance Log Web Service
    /// </summary>
    public class AttendanceLogWS : AHealthCheck
    {
        // Connection Variables
        private const String BASE_PATH = "https://attendanceapikea2018.herokuapp.com";
        private const String BASE_PORT = "";
        private const String BASE_POST = "/insertcourseregistration";
        private const String BASE_HEALTH = "/health";
        private String destination;

        public AttendanceLogWS() : base("AttendanceLogWS")
        {
        }

        /// <summary>
        /// Adds data to attendance log ws
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>String response</returns>
        public string addToAttendanceLog(JObject data)
        {
            destination = BASE_PATH + BASE_PORT + BASE_POST;
            WebClient client = new WebClient();
            Debug.WriteLine("Destination: " + destination);
            Debug.WriteLine("Data: " + data.ToString());

            NameValueCollection nvCollection = new NameValueCollection();
            nvCollection.Add("StudentID", (String)data["studentID"]);
            nvCollection.Add("CourseID", (String)data["courseid"]);
            nvCollection.Add("successAttempt", (String)data["success"]);
            // Add params to body

            // Send data and get response
            byte[] response = client.UploadValues(destination, "POST", nvCollection);

            // return response in string
            return Encoding.ASCII.GetString(response);
        }

        public override string CheckHealth()
        {
            destination = BASE_PATH + BASE_PORT + BASE_HEALTH;
            WebClient client = new WebClient();
            Debug.WriteLine("Destination: " + destination);

            String response = "";
            // Creating stream to endpoint
            try
            {
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
