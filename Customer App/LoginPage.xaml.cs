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
                YogaClassDB db = app.YogaClassDatabase;
                User currentUser = db.GetUser(emailTxt.Text);

				if (currentUser == null) 
                {
                    User user = new User();
                    user.Email = emailTxt.Text;
                    db.SaveUser(user);
                    app.User = user;
				} 
                else
                {
                    app.User = currentUser;
				}
                Shell.Current.GoToAsync("///ClassListPage");
            }
        }
    }
}