namespace Customer_App;

public partial class ClassListPage : ContentPage
{
	public ClassListPage()
	{
		InitializeComponent();
		List<string> classes = new() {
			"Aerial Yoga",
			"Flow Yoga",
			"Family Yoga",
			"Meditation"
		};

		classList.ItemsSource = classes;
	}
}