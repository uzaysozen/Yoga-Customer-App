namespace Customer_App
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnSignIn(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailTxt.Text))
            {
                DisplayAlert("Error", "Email is required", "OK");
            }
            else
            {
                App app = Application.Current as App;
                User user = new User
                {
                    userId = emailTxt.Text
                };
                app.User = user;
                app.User.bookingList = new List<Instance>();
                app.ShoppingCart = new();
				
                Shell.Current.GoToAsync("///ClassListPage");
            }
        }

    }
}