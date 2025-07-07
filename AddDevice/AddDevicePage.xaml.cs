using MobileApp.ViewModels;
using Microsoft.Maui.Controls;
using System;
using MobileApp.Models;
using MobileApp.ServiceAPI;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


namespace MobileApp
{
	public partial class AddDevicePage : ContentPage
	{
		public AddDevicePage()
		{
			InitializeComponent();
			LoadRoomData();  
		
		}
		private Dictionary<string, List<Room>> roomMapping = new();
		private List<string> distinctRoomTypes = new();
		private HttpClient _httpClient;

		private async void LoadRoomData()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("http://10.0.2.2:44346/");
		

			try
			{
				var rooms = await _httpClient.GetFromJsonAsync<List<Room>>("api/room");
				if (rooms != null)
				{
					roomMapping = rooms
						.GroupBy(r => r.room_type)
						.ToDictionary(g => g.Key, g => g.ToList());

					distinctRoomTypes = roomMapping.Keys.ToList();

					// GÁN DỮ LIỆU CHO PICKER
					pickerRoomType.ItemsSource = distinctRoomTypes;

				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Lỗi tải dữ liệu", ex.Message, "OK");
			}
		}
		private void pickerRoomType_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedType = pickerRoomType.SelectedItem as string;
			if (selectedType != null && roomMapping.ContainsKey(selectedType))
			{
				var rooms = roomMapping[selectedType];
				pickerRoom.ItemsSource = rooms;
				pickerRoom.ItemDisplayBinding = new Binding("room_id");
				pickerRoom.SelectedIndex = -1;
			}
		}

		private async void OnSaveClicked(object sender, EventArgs e)
		{
			var selectedRoom = pickerRoom.SelectedItem as Room;
			if (selectedRoom == null)
			{
				await DisplayAlert("Lỗi", "Chưa chọn vị trí tiếp nhận (Phòng)", "OK");
				return;
			}

			string roomId = selectedRoom.room_id.ToString();
			string statusId = selectedRoom.room_id == "KHO" ? "30" : "20";
			var newDevice = new MedDevice
			{
				device_id = GenerateDeviceId(entryDeviceType.Text, entryManufacturer.Text, entrySeri.Text), // Hoặc tự tạo ID theo format bạn cần
				device_name = entryDeviceName.Text,
				device_manufacturer = entryManufacturer.Text,
				device_seri = entrySeri.Text, // Có thể tạo theo vòng lặp nếu nhiều series
				device_type = entryDeviceType.Text,
				device_group = pickerGroup.SelectedItem?.ToString(),
				device_maintenance_start = dateMaintenanceStart.Date,
				device_maintenance_cycle = int.TryParse(entryMaintenanceCycle.Text, out var cycle) ? cycle : 0,
				device_condition = pickerCondition.SelectedItem?.ToString(),
				device_received_date = dateReceived.Date,
				device_note = entryNote.Text,
				FK_status_id = statusId,
				FK_room_id = roomId,
				FK_contact_id = null,
				device_stockout_date = null
			};
			if (string.IsNullOrEmpty(roomId))
			{
				await DisplayAlert("Lỗi", "Chưa chọn vị trí tiếp nhận (Phòng)", "OK");
				return;
			}
			var deviceService = new DeviceService();
			var result = await deviceService.AddDeviceAsync(newDevice);

			if (result)
			{
				await DisplayAlert("Thành công", "Thiết bị đã được thêm", "OK");
				await Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Lỗi", "Không thể thêm thiết bị", "OK");
			}
		}
		private string GenerateDeviceId(string type, string manufacturer, string seri)
		{
			string manuPart = (manufacturer?.Length >= 4) ? manufacturer.Substring(0, 4).ToUpper() : manufacturer?.ToUpper();
			string seriPart = (seri?.Length >= 4) ? seri.Substring(seri.Length - 4) : seri;
			return $"{type}-{manuPart}-{seriPart}";
		}
	}
}