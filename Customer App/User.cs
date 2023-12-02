using System.ComponentModel.DataAnnotations;

namespace Customer_App
{
    public class User
    {
		[EmailAddress]
		public String Email {  get; set; }
    }
}
