namespace Customer_App;

public partial class NewPage2 : ContentPage
{
	public NewPage2()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		Rest_Client client = new Rest_Client();
		
		string result = await client.SendEventAsync();
		lblResult.Text = result;
    }
}