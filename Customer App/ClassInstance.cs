using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_App
{
    [Table("ClassInstance")]
    public class ClassInstance
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        [Unique]
        public int ClassId { get; set; }
        public DateTime ClassDate { get; set; }  
        public string TeacherName { get; set; }
        public string DayOfTheWeek { get; set; }
        public string ClassTime { get; set; }

    }
}
