using Newtonsoft.Json.Linq;
namespace MOM.WebServiceControllers
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Net;
    using System.Text;

    /// <summary>
    /// The connection to the Attendance Log Web Service
    /// </summary>
    public class AttendanceLogWS
    {
        // Connection Variables
        private const String BASE_PATH = "http://localhost";
        private const String BASE_PORT = ":8000";
        private const String BASE_POST = "/insertcourseregistration";
        private String destination;

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
    }
}
