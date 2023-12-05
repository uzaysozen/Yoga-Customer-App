namespace Customer_App;

public partial class ClassListPage : ContentPage
{

	private readonly App app;
	private readonly YogaClassDB db;
	public ClassListPage()
	{
		InitializeComponent();
		
		app = Application.Current as App;
		db = app.YogaClassDatabase;
        Rest_Client client = new Rest_Client();

        List<ClassInstance> classes = db.GetClassInstanceList();

		ClassInstancesListView.ItemsSource = classes;
	}

	private void AddToCart_Clicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var classInstance = button.BindingContext as ClassInstance;

		var existingItem = app.ShoppingCart.FirstOrDefault(ci => ci.Id == classInstance.Id);

		if (existingItem == null)
		{
			app.ShoppingCart.Add(classInstance);
		}
		else
		{
			DisplayAlert("Item Exists", "This item is already in your shopping cart.", "OK");
		}
	}

	private void OnTextChanged(object sender, EventArgs e)
	{
		SearchBar searchBar = (SearchBar)sender;
		if (SearchSwitch.IsToggled)
		{
			ClassInstancesListView.ItemsSource = db.SearchClassInstanceByDayOfWeek(searchBar.Text);
		} 
		else
		{
			ClassInstancesListView.ItemsSource = db.SearchClassInstanceByTimeOfDay(searchBar.Text);
		}
			
	}

	private void OnSearchSwitchToggled(object sender, EventArgs e)
	{
		Switch searchSwitch = (Switch)sender;
		if (searchSwitch.IsToggled)
		{
			SearchLabel.Text = "Search by Day";
		} 
		else
		{
			SearchLabel.Text = "Search by Time";
		}
	}

}