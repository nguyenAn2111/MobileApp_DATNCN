using MobileApp.ViewModels;
using Microsoft.Maui.Controls;
using System;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;

namespace MobileApp
{
	public partial class MainPage : ContentPage
	{
		private readonly DeviceViewModel _viewModel;

		public MainPage()
		{
			InitializeComponent();

			_viewModel = new DeviceViewModel(); // Tạo instance
			BindingContext = _viewModel;        // Gán BindingContext

			Loaded += MainPage_Loaded;
		}

		private async void MainPage_Loaded(object sender, EventArgs e)
		{
			await _viewModel.LoadDevices();
		}
		private async void OnAddDeviceClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new AddDevicePage());
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await _viewModel.LoadDevices(); // Load lại thiết bị mỗi lần trở lại trang
		}
		private async void OnRefreshDevices(object sender, EventArgs e)
		{
			await _viewModel.LoadDevices(); // Load lại danh sách
			refreshView.IsRefreshing = false; // Dừng xoay
		}
	}

}