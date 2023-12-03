using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Customer_App
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

		[Unique, Required, EmailAddress]
		public string Email {  get; set; }

	}
}
