using System;
using MobileApp.ViewModels;
using Microsoft.Maui.Controls;

namespace MobileApp
{
	public partial class StoragePage : ContentPage
	{
		private readonly StorageViewModel _viewModel;

		public StoragePage()
		{
			InitializeComponent();
			_viewModel = new StorageViewModel();
			BindingContext = _viewModel;
			Loaded += StoragePage_Loaded;
		}

		private async void StoragePage_Loaded(object sender, EventArgs e)
		{
			await _viewModel.LoadDataAsync();
		}
	}
}