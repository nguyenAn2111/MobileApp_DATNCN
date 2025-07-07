using System.Collections.ObjectModel;
using System.ComponentModel;
using MobileApp.Models;
using MobileApp.ServiceAPI;

namespace MobileApp.ViewModels
{
	public class DeviceViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<MedDevice> Devices { get; set; } = new();

		private readonly DeviceService _deviceService = new();

		public DeviceViewModel()
		{
			LoadDevices();
		}

		public async Task LoadDevices()
		{
			var list = await _deviceService.GetDevicesAsync();

			var sortedList = list
	   .OrderBy(d => d.device_received_date ?? DateTime.MinValue)
	   .ToList();

			Devices.Clear();
			foreach (var device in sortedList)
			{
				Devices.Add(device);
			}

			Console.WriteLine("[DEBUG] Devices loaded: " + Devices.Count); // <-- kiểm tra
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
