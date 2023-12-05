namespace Customer_App;

public partial class ProfilePage : ContentPage
{

	private App app;

	public ProfilePage()
	{
		
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		app = Application.Current as App;
		userEmailTxt.Text = app.User.userId;
	}

	private void OnSignOut(object sender, EventArgs e)
	{
		if (app.User != null)
		{
            app.User = null;
            app.ShoppingCart = null;
        }
		
		
		Shell.Current.GoToAsync("///LoginPage");
		//Navigation.PopToRootAsync();
	}
}