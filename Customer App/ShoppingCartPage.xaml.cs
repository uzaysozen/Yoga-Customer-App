namespace Customer_App;

public partial class ShoppingCartPage : ContentPage
{
	private App app;

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
}
