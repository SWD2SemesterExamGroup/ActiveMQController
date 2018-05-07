using MOM.KEA_Organization;

namespace MOM.ActiveMQ.Teacher
{
    using Newtonsoft.Json;
    using System;
    using System.Web.Services.Protocols;

    public class WSConsumer
    {
        private Teachersws ws_kea = new Teachersws();

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
                Console.WriteLine(eSoap.ToString());
            }

            Console.WriteLine("Teacher WS data");
            Console.WriteLine(teacherEntity.ToString());

            return teacherEntity;
        }
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

            Console.WriteLine("Teacher WS data");
            Console.WriteLine(teacherEntity.ToString());

            Console.WriteLine("Teacher WS data To Json");

            string result = JsonConvert.SerializeObject(teacherEntity);
            Console.WriteLine("Done converting teacher Json");
            //Console.WriteLine(result);

            // DIDN'T WORK
            /*XmlDocument teacherDoc = new XmlDocument();
            teacherDoc.LoadXml(teacherEntity.ToString());
            string result = JsonConvert.SerializeObject(teacherDoc);
            Console.WriteLine("Done converting teacher Json");
            Console.WriteLine(result);*/

            return result;
        }

    }
}
