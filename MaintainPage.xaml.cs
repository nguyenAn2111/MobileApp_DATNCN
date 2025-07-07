using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using MobileApp.ViewModels;

namespace MobileApp
{
	public partial class MaintainPage : ContentPage
	{
		private readonly MaintainViewModel _viewModel;

		public MaintainPage()
		{
			InitializeComponent();
			_viewModel = new MaintainViewModel();
			BindingContext = _viewModel;
			Loaded += async (_, _) =>
			{
				await _viewModel.LoadMaintainsAsync();
				Console.WriteLine("Loaded xong");
			};
		}
	}
	
}
	
