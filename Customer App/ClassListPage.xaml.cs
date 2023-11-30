namespace Customer_App;

public partial class ClassListPage : ContentPage
{
	public ClassListPage()
	{
		InitializeComponent();
	}

    private void BackBtnClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}