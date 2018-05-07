using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOM.Models.Teacher
{
    public class TeacherModel
    {
        public int TeacherID { get; set; }
        public List<CourseModel> ListCourses { get; set; }
        public List<ClassModel> ListClasses { get; set; }

        public TeacherModel()
        {

        }

        public TeacherModel(String json)
        {

        }
    }
}
