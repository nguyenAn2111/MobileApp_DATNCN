using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MobileApp.Models;
using MobileApp.Services;

namespace MobileApp.ViewModels
{
	public class StorageViewModel : INotifyPropertyChanged
	{
		private readonly StorageService _service = new();

		public ObservableCollection<MedDevice> DevicesImport { get; set; } = new();
		public ObservableCollection<MedDevice> DevicesExport { get; set; } = new();
		public ObservableCollection<MedDevice> DevicesAll { get; set; } = new();
		public ObservableCollection<Room> RoomList { get; set; } = new();

		private MedDevice _selectedDeviceImport;
		public MedDevice SelectedDeviceImport
		{
			get => _selectedDeviceImport;
			set
			{
				_selectedDeviceImport = value;
				OnPropertyChanged();

				var currentRoom = RoomList?.FirstOrDefault(r => r.room_id == value?.FK_room_id);
				ImportRoomFrom = currentRoom?.room_name ?? "(Không xác định)";
			}
		}

		private MedDevice _selectedDeviceTransfer;
		public MedDevice SelectedDeviceTransfer
		{
			get => _selectedDeviceTransfer;
			set
			{
				_selectedDeviceTransfer = value;
				OnPropertyChanged();

				var currentRoom = RoomList?.FirstOrDefault(r => r.room_id == value?.FK_room_id);
				TransferRoomFrom = currentRoom?.room_name ?? "(Không xác định)";
			}
		}
		public MedDevice SelectedDeviceExport { get; set; }


		public Room SelectedExportRoom { get; set; }
		public Room SelectedRoomTo { get; set; }

		private string _importRoomFrom;
		public string ImportRoomFrom
		{
			get => _importRoomFrom;
			set
			{
				_importRoomFrom = value;
				OnPropertyChanged();
			}
		}

		private string _transferRoomFrom;
		public string TransferRoomFrom
		{
			get => _transferRoomFrom;
			set
			{
				_transferRoomFrom = value;
				OnPropertyChanged();
			}
		}


		public DateTime ImportDate { get; set; } = DateTime.Today;
		public DateTime ExportDate { get; set; } = DateTime.Today;
		public DateTime TransferDate { get; set; } = DateTime.Today;

		public ICommand ImportCommand { get; }
		public ICommand ExportCommand { get; }
		public ICommand TransferCommand { get; }

		public StorageViewModel()
		{
			ImportCommand = new Command(async () => await ImportDeviceAsync());
			ExportCommand = new Command(async () => await ExportDeviceAsync());
			TransferCommand = new Command(async () => await TransferDeviceAsync());

			_ = LoadDataAsync();
		}

		public async Task LoadDataAsync()
		{
			var devices = await _service.GetAllDevices();
			var rooms = await _service.GetAllRooms();

			RoomList = new ObservableCollection<Room>(rooms);
			OnPropertyChanged(nameof(RoomList));

			// Giả sử FK_room_id là kiểu string, và cần so sánh với "KHO"
			var devicesInKho = devices
				.Where(d => string.Equals(d.FK_room_id, "KHO", StringComparison.OrdinalIgnoreCase))
				.ToList();

			var devicesNotInKho = devices
				.Where(d => !string.Equals(d.FK_room_id, "KHO", StringComparison.OrdinalIgnoreCase))
				.ToList();

			DevicesExport = new ObservableCollection<MedDevice>(devicesInKho);       // Xuất từ kho
			DevicesImport = new ObservableCollection<MedDevice>(devicesNotInKho);   // Nhập vào kho
			DevicesAll = new ObservableCollection<MedDevice>(devices);              // Tất cả (dùng cho Transfer)

			OnPropertyChanged(nameof(DevicesImport));
			OnPropertyChanged(nameof(DevicesExport));
			OnPropertyChanged(nameof(DevicesAll));
		}

		private async Task ImportDeviceAsync()
		{
			if (SelectedDeviceImport == null)
			{
				await Application.Current.MainPage.DisplayAlert("⚠️", "Vui lòng chọn thiết bị để nhập kho", "OK");
				return;
			}

			var request = new StorageRequest
			{
				device_id = SelectedDeviceImport.device_id,
				date = ImportDate.ToString("yyyy-MM-dd")
			};
			await _service.ImportDevice(request);
			await Application.Current.MainPage.DisplayAlert("✅", "Đã ghi nhận nhập kho", "OK");
		}

		private async Task ExportDeviceAsync()
		{
			if (SelectedDeviceExport == null || SelectedExportRoom == null)
			{
				await Application.Current.MainPage.DisplayAlert("⚠️", "Vui lòng chọn thiết bị và phòng xuất kho", "OK");
				return;
			}

			var request = new StorageRequest
			{
				device_id = SelectedDeviceExport.device_id,
				room_to = SelectedExportRoom.room_id,
				date = ExportDate.ToString("yyyy-MM-dd")
			};
			await _service.ExportDevice(request);
			await Application.Current.MainPage.DisplayAlert("✅", "Đã ghi nhận xuất kho", "OK");
		}

		private async Task TransferDeviceAsync()
		{
			if (SelectedDeviceTransfer == null || SelectedRoomTo == null)
			{
				await Application.Current.MainPage.DisplayAlert("⚠️", "Vui lòng chọn thiết bị và phòng đích", "OK");
				return;
			}

			var request = new StorageRequest
			{
				device_id = SelectedDeviceTransfer.device_id,
				room_to = SelectedRoomTo.room_id,
				date = TransferDate.ToString("yyyy-MM-dd")
			};
			await _service.TransferDevice(request);
			await Application.Current.MainPage.DisplayAlert("✅", "Đã ghi nhận điều chuyển thiết bị", "OK");
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = "") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}