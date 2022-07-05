using System;
using System.Collections.Generic;

namespace UMS.Persistence.Models
{
    public partial class SessionTime
    {
        public SessionTime()
        {
            TeacherPerCoursePerSessionTimes = new HashSet<TeacherPerCoursePerSessionTime>();
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Id { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<TeacherPerCoursePerSessionTime> TeacherPerCoursePerSessionTimes { get; set; }
    }
}
