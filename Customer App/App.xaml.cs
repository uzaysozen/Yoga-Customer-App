using System.Collections.ObjectModel;

namespace Customer_App
{
    public partial class App : Application
    {
        public User User { get; set; }
        public ObservableCollection<ClassInstance> ShoppingCart = new();

        private YogaClassDB yogaClassDB;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

        }

        public YogaClassDB YogaClassDatabase
        {
			get
			{
				yogaClassDB ??= new YogaClassDB(); 
                //yogaClassDB.ResetDatabase();
				yogaClassDB.SeedDatabase();
                return yogaClassDB;
            }
        }
    }
}