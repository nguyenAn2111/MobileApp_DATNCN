using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MobileApp.Models;
using System.Windows.Input;
using System;
using MobileApp.ServiceAPI;

namespace MobileApp.ViewModels;

public class MaintainViewModel : INotifyPropertyChanged
{
	public ObservableCollection<Maintain> Maintains { get; set; } = new();
	private readonly MaintainService _service = new();
	private readonly DeviceService _deviceService = new(); // Service lấy danh sách thiết bị

	public List<MedDevice> DeviceList { get; set; } = new();

	private MedDevice _selectedDevice;
	public MedDevice SelectedDevice
	{
		get => _selectedDevice;
		set
		{
			_selectedDevice = value;
			OnPropertyChanged();
		}
	}

	public DateTime MaintainDate { get; set; } = DateTime.Today;
	public string DeliveryPerson { get; set; }
	public string DeliveryPhone { get; set; }
	public string Technician { get; set; }
	public string TechnicianPhone { get; set; }
	public string MaintainCost { get; set; }

	public ICommand SaveMaintainCommand { get; }

	public MaintainViewModel()
	{
		_service = new MaintainService();
		Maintains = new ObservableCollection<Maintain>();
		SaveMaintainCommand = new Command(async () => await SaveMaintainAsync());

		_ = LoadMaintainsAsync();
		_ = LoadDevicesAsync(); // 👈 Thiết bị không hiển thị nếu thiếu dòng này
	}

	public async Task LoadMaintainsAsync()
	{
		var list = await _service.GetMaintainsAsync();

		Console.WriteLine($"👉 Maintains count = {list?.Count}");

		foreach (var item in list)
			Console.WriteLine($"ID = {item.device_id}, Name = {item.device_name}");

		Maintains = new ObservableCollection<Maintain>(list ?? new());
		OnPropertyChanged(nameof(Maintains));
	}

	private async Task LoadDevicesAsync()
	{
		var allDevices = await _deviceService.GetDevicesAsync();

		// Lọc danh sách thiết bị
		DeviceList = allDevices?.Where(d =>
			d.FK_status_id == "00" ||
			(d.status_name != null && d.status_name.Equals("chưa xếp lịch", StringComparison.OrdinalIgnoreCase))
		).ToList() ?? new();

		OnPropertyChanged(nameof(DeviceList));
	}

	private async Task SaveMaintainAsync()
	{
		if (SelectedDevice == null)
		{
			await Application.Current.MainPage.DisplayAlert("⚠️", "Vui lòng chọn thiết bị", "OK");
			return;
		}

		var maintain = new Maintain
		{
			FK_device_id = SelectedDevice.device_id,
			maintain_date = MaintainDate,
			maintain_delivery = DeliveryPerson,
			maintain_delivery_phone = DeliveryPhone,
			maintain_maintenance = Technician,
			maintain_maintenance_phone = TechnicianPhone,
			contact_address = "MaintainContact",
			contact_finance = MaintainCost
		};

		bool success = await _service.CreateMaintainAsync(maintain);
		if (success)
		{
			await Application.Current.MainPage.DisplayAlert("✅", "Đã lưu kế hoạch bảo trì", "OK");
			await LoadMaintainsAsync();
		}
		else
		{
			await Application.Current.MainPage.DisplayAlert("❌", "Lỗi khi lưu thông tin", "OK");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string name = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
