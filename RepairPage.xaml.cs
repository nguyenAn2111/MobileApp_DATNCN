using System;
using System.Globalization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using MobileApp.ViewModels;

namespace MobileApp
{
	public partial class RepairPage : ContentPage
	{
		private readonly RepairViewModel _viewModel;

		public RepairPage()
		{
			InitializeComponent();
			_viewModel = new RepairViewModel();
			BindingContext = _viewModel;
		}
		

	}
}