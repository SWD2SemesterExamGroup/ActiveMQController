namespace MOM.Helpers.ContentFilters
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// A filter class for the Course Access Web Service(CAWS)
    /// Filter message from teacher to be ready for CAWS
    /// </summary>
    public class PHPWSFilter
    {
        // Implement filter to php ws when ready
        /// <summary>
        /// Filters to web service.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>JObject - from String</returns>
        public JObject filterToWS(String message)
        {
            // For test of Course Access WS
            //message = getMessage();
            //Console.WriteLine("Filter");
            Debug.WriteLine("Filter");
            JObject json = JObject.Parse(message);
            //Console.WriteLine(json.ToString());
            //Debug.WriteLine(json.ToString());
            // Lessons Array
            var keys = from sp in json["listKeys"] select sp;
            Debug.WriteLine("Linq list of Keys");
            //Console.WriteLine(keys);
            //Debug.WriteLine(keys);
            //Debug.WriteLine("Type of Keys");
            //Console.WriteLine(keys.GetType());
            //Debug.WriteLine(keys.GetType());

            // Get TeacherID, keys, classID, courseID, startdate, expiredate
            Debug.WriteLine("Parsing to new JObject ready for parse to php ws");

            JObject teacherPHPParser = new JObject();
            teacherPHPParser["teacherid"] = (int)json["teacherViewPersist"]["teacherModel"]["teacherID"];
            teacherPHPParser["courseid"] = (int)json["teacherViewPersist"]["courseID"];
            teacherPHPParser["classid"] = (int)json["teacherViewPersist"]["classID"];
            JArray jkeys = new JArray(keys);
            teacherPHPParser["keys"] = jkeys;

            return teacherPHPParser;
        }

        /// <summary>
        /// Premade String for test
        /// </summary>
        /// <returns>String</returns>
        private String getMessage()
        {
            return "{'teacherViewPersist':{'teacherModel':{'teacherID':1,'teacherName':'Troels Helbo Jensen','courses':[{'@ID':1,'title':'Construction 1','courseClass':{'@ID':2,'title':'SWD Team 11','courseClass':null,'id':1},'id':1},{'@ID':3,'title':'Python','courseClass':{'@ID':4,'title':'Datamatiker 17B','courseClass':null,'id':2},'id':3}]},'listClasses':[4],'teacherID':0,'courseID':3,'classID':2,'startTimeID':0,'noOfLessons':5,'courseTimeSchedule':{'startPoints':[{'id':0,'timeDisplay':'08:30','date':'2018-05-16'},{'id':1,'timeDisplay':'08:45','date':'2018-05-16'},{'id':2,'timeDisplay':'9:00','date':'2018-05-16'},{'id':3,'timeDisplay':'9:15','date':'2018-05-16'},{'id':4,'timeDisplay':'9:30','date':'2018-05-16'},{'id':5,'timeDisplay':'9:45','date':'2018-05-16'},{'id':6,'timeDisplay':'10:00','date':'2018-05-16'},{'id':7,'timeDisplay':'10:15','date':'2018-05-16'},{'id':8,'timeDisplay':'10:30','date':'2018-05-16'},{'id':9,'timeDisplay':'10:45','date':'2018-05-16'},{'id':10,'timeDisplay':'11:00','date':'2018-05-16'},{'id':11,'timeDisplay':'11:15','date':'2018-05-16'},{'id':12,'timeDisplay':'11:30','date':'2018-05-16'},{'id':13,'timeDisplay':'11:45','date':'2018-05-16'},{'id':14,'timeDisplay':'12:00','date':'2018-05-16'},{'id':15,'timeDisplay':'12:15','date':'2018-05-16'},{'id':16,'timeDisplay':'12:30','date':'2018-05-16'},{'id':17,'timeDisplay':'12:45','date':'2018-05-16'},{'id':18,'timeDisplay':'13:00','date':'2018-05-16'},{'id':19,'timeDisplay':'13:15','date':'2018-05-16'},{'id':20,'timeDisplay':'13:30','date':'2018-05-16'},{'id':21,'timeDisplay':'13:45','date':'2018-05-16'},{'id':22,'timeDisplay':'14:00','date':'2018-05-16'},{'id':23,'timeDisplay':'14:15','date':'2018-05-16'},{'id':24,'timeDisplay':'14:30','date':'2018-05-16'},{'id':25,'timeDisplay':'14:45','date':'2018-05-16'},{'id':26,'timeDisplay':'15:00','date':'2018-05-16'},{'id':27,'timeDisplay':'15:15','date':'2018-05-16'},{'id':28,'timeDisplay':'15:30','date':'2018-05-16'},{'id':29,'timeDisplay':'15:45','date':'2018-05-16'},{'id':30,'timeDisplay':'16:00','date':'2018-05-16'},{'id':31,'timeDisplay':'16:15','date':'2018-05-16'},{'id':32,'timeDisplay':'16:30','date':'2018-05-16'},{'id':33,'timeDisplay':'16:45','date':'2018-05-16'},{'id':34,'timeDisplay':'17:00','date':'2018-05-16'},{'id':35,'timeDisplay':'17:15','date':'2018-05-16'},{'id':36,'timeDisplay':'17:30','date':'2018-05-16'},{'id':37,'timeDisplay':'17:45','date':'2018-05-16'},{'id':38,'timeDisplay':'18:00','date':'2018-05-16'},{'id':39,'timeDisplay':'18:15','date':'2018-05-16'},{'id':40,'timeDisplay':'18:30','date':'2018-05-16'},{'id':41,'timeDisplay':'18:45','date':'2018-05-16'},{'id':42,'timeDisplay':'19:00','date':'2018-05-16'},{'id':43,'timeDisplay':'19:15','date':'2018-05-16'},{'id':44,'timeDisplay':'19:30','date':'2018-05-16'},{'id':45,'timeDisplay':'19:45','date':'2018-05-16'},{'id':46,'timeDisplay':'20:00','date':'2018-05-16'},{'id':47,'timeDisplay':'20:15','date':'2018-05-16'},{'id':48,'timeDisplay':'20:30','date':'2018-05-16'},{'id':49,'timeDisplay':'20:45','date':'2018-05-16'}],'lessons':[0,1,2,3,4,5,6,7]}},'listKeys':[{'index':1,'key':'z*;:pv&rAf','startPoint':{'id':0,'timeDisplay':'08:30','date':'2018-05-16'},'expirationPoint':{'id':6,'timeDisplay':'10:00','date':'2018-05-16'}},{'index':0,'key':'BREAK','startPoint':{'id':6,'timeDisplay':'10:00','date':'2018-05-16'},'expirationPoint':{'id':7,'timeDisplay':'10:15','date':'2018-05-16'}},{'index':2,'key':'n6YwRDk:uN','startPoint':{'id':8,'timeDisplay':'10:30','date':'2018-05-16'},'expirationPoint':{'id':14,'timeDisplay':'12:00','date':'2018-05-16'}},{'index':0,'key':'BREAK','startPoint':{'id':14,'timeDisplay':'12:00','date':'2018-05-16'},'expirationPoint':{'id':15,'timeDisplay':'12:15','date':'2018-05-16'}},{'index':3,'key':'o-JAuvYIPX','startPoint':{'id':15,'timeDisplay':'12:15','date':'2018-05-16'},'expirationPoint':{'id':21,'timeDisplay':'13:45','date':'2018-05-16'}}]}";
        }
    }
}
