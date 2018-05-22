using MOM.KEA_Organization;

namespace MOM.ActiveMQ.Teacher
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.Web.Services.Protocols;

    /// <summary>
    /// Connection and functionality class for the KEA Organization Web Service
    /// </summary>
    public class WSConsumer
    {
        /// <summary>
        /// Field for
        /// </summary>
        private Teachersws ws_kea = new Teachersws();

        /// <summary>
        /// Gets the teacher entity by.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Object TeacherEntityView
        /// </returns>
        public teacherEntityView getTeacherEntityBy(int id)
        {
            teacherEntityView teacherEntity = new teacherEntityView();

            try
            {
                teacherEntity = ws_kea.CourseClassesDataBy(id);
                //ws_kea.Dispose();
            }
            catch (SoapHeaderException eSoap)
            {
                Debug.WriteLine(eSoap.ToString());
            }

            //Console.WriteLine("Teacher WS data");
            //Console.WriteLine(teacherEntity.ToString());

            // return id or 0
            return teacherEntity;
        }
        /// <summary>
        /// Gets the teacher entity json by.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>String</returns>
        public String getTeacherEntityJsonBy(int id)
        {
            teacherEntityView teacherEntity = new teacherEntityView();
            try
            {
                teacherEntity = ws_kea.CourseClassesDataBy(id);
                //ws_kea.Dispose();
            }
            catch (SoapHeaderException eSoap)
            {
                Console.WriteLine(eSoap.ToString());
            }

            //Console.WriteLine("Teacher WS data");
            //Console.WriteLine(teacherEntity.ToString());

            //Console.WriteLine("Teacher WS data To Json");

            // Serialize response
            string result = JsonConvert.SerializeObject(teacherEntity);
            Console.WriteLine("Done converting teacher Json");
            //Console.WriteLine(result);
            
            return result;
        }
    }
}
