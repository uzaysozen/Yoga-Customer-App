namespace Customer_App
{
    public partial class App : Application
    {
        public User User { get; set; }
        public App()
        {
            InitializeComponent();
            User = new();
            MainPage = new AppShell();
        }
    }
}