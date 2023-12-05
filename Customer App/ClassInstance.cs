using SQLite;

namespace Customer_App
{
    [Table("ClassInstance")]
    public class ClassInstance
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        [Unique]
        public int instanceId { get; set; }
        public string date { get; set; }  
        public string teacher { get; set; }
        public string classDay { get; set; }
        public string classTime { get; set; }

    }
}
