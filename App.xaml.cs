namespace MobileApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			// Dùng AppShell làm giao diện chính
			MainPage = new AppShell();
		}
	}
}