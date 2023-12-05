
namespace Customer_App;

public partial class ShoppingCartPage : ContentPage
{
    public List<ClassInstance> ShoppingEventResponse { get; set; }

    private App app;
	BookingResponse bookingResponse;

    public ShoppingCartPage()
	{
		InitializeComponent();

    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		app = Application.Current as App;
		ShoppingCartCollectionView.ItemsSource = app.ShoppingCart;
	}


	private void Remove_Clicked(object sender, EventArgs e)
	{
        var button = sender as Button;
		var classInstance = button.BindingContext as ClassInstance;

		if (app.ShoppingCart.Contains(classInstance))
		{
			app.ShoppingCart.Remove(classInstance);
		}
	}

	private async void Book_Class(object sender, EventArgs e)
	{

        Rest_Client client = new Rest_Client();
		foreach (var item in app.ShoppingCart)
		{
            app.User.bookingList.Add(new Instance() { instanceId = item.instanceId });
        }

		

        bookingResponse = await client.SendBookingAsync();


		await DisplayAlert("Booking Successful!", $"{bookingResponse.uploadResponseCode}\n{bookingResponse.userId}\n{bookingResponse.bookings}\n{bookingResponse.message}", "OK");

        app.ShoppingCart.Clear();
        app.User.bookingList.Clear();
    }
}
