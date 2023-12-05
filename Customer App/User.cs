using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Customer_App
{
    public class User
    {
		public string userId {  get; set; }
        public List<Instance> bookingList { get; set; }
	}
}
