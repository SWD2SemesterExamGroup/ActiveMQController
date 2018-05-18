namespace MOM.Models.Teacher
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class TeacherModel
    {
        /// <summary>
        /// Gets or sets the teacher identifier.
        /// </summary>
        /// <value>
        /// The teacher identifier.
        /// </value>
        public int TeacherID { get; set; }
        /// <summary>
        /// Gets or sets the list courses.
        /// </summary>
        /// <value>
        /// The list courses.
        /// </value>
        public List<CourseModel> ListCourses { get; set; }
        /// <summary>
        /// Gets or sets the list classes.
        /// </summary>
        /// <value>
        /// The list classes.
        /// </value>
        public List<ClassModel> ListClasses { get; set; }

        // Constructors
        public TeacherModel()
        {
        }
        public TeacherModel(String json)
        {
        }
    }
}
