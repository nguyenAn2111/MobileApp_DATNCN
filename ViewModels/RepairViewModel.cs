using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MobileApp.Models;
using System.Windows.Input;
using System;
using MobileApp.ServiceAPI;

public class RepairViewModel : INotifyPropertyChanged
{
	public ObservableCollection<Repair> Repairs { get; set; } = new();
	private readonly RepairService _service = new();
	private readonly DeviceService _deviceService = new(); // 👈 Thêm service lấy danh sách thiết bị

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

	public int Priority { get; set; }
	public string RepairCost { get; set; }
	public DateTime BrokenDate { get; set; } = DateTime.Today;
	public DateTime RepairDate { get; set; } = DateTime.Today;
	public string Note { get; set; }

	public ICommand SaveRepairCommand { get; }

	public RepairViewModel()
	{
		SaveRepairCommand = new Command(async () => await SaveRepairAsync());
		_ = LoadRepairsAsync();
		_ = LoadDevicesAsync();
	}

	public async Task LoadRepairsAsync()
	{
		var list = await _service.GetRepairsAsync();
		Repairs = new ObservableCollection<Repair>(list ?? new());
		OnPropertyChanged(nameof(Repairs));
	}

	private async Task LoadDevicesAsync()
	{
		var allDevices = await _deviceService.GetDevicesAsync();

		// Chỉ lấy thiết bị đang ở trạng thái "đang sử dụng" hoặc "đang lỗi"
		DeviceList = allDevices?.Where(d =>
			d.FK_status_id == "20" || d.FK_status_id == "21"
		).ToList() ?? new();

		OnPropertyChanged(nameof(DeviceList));
	}

	private async Task SaveRepairAsync()
	{
		if (SelectedDevice == null)
		{
			await Application.Current.MainPage.DisplayAlert("⚠️", "Vui lòng chọn thiết bị", "OK");
			return;
		}

		var repair = new Repair
		{
			FK_device_id = SelectedDevice.device_id,
			repair_priority = Priority,
			repair_broken = BrokenDate,
			repair_date = RepairDate,
			repair_note = Note,
			contact_finance = RepairCost,
			contact_address = "repaircontact",
			repair_picture = null
		};

		bool success = await _service.CreateRepairAsync(repair);
		if (success)
		{
			await Application.Current.MainPage.DisplayAlert("✅", "Đã tạo yêu cầu sửa chữa", "OK");
			await LoadRepairsAsync();
		}
		else
		{
			await Application.Current.MainPage.DisplayAlert("❌", "Tạo yêu cầu thất bại", "OK");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;
	void OnPropertyChanged([CallerMemberName] string name = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
