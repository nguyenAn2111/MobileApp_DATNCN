using System.Net.Http.Json;
using MobileApp.Models;

namespace MobileApp.Services
{
	public class StorageService
	{
		private readonly HttpClient _http = new HttpClient();
		private const string BaseUrl = "http://10.0.2.2:44346/api/storageapi";

		public async Task<List<MedDevice>> GetAllDevices()
		{
			return await _http.GetFromJsonAsync<List<MedDevice>>("http://10.0.2.2:44346/api/deviceapi") ?? new();
		}

		public async Task<List<Room>> GetAllRooms()
		{
			return await _http.GetFromJsonAsync<List<Room>>("http://10.0.2.2:44346/api/roomapi") ?? new();
		}

		public async Task ImportDevice(StorageRequest request)
		{
			await _http.PostAsJsonAsync($"{BaseUrl}/import", request);
		}

		public async Task ExportDevice(StorageRequest request)
		{
			await _http.PostAsJsonAsync($"{BaseUrl}/export", request);
		}

		public async Task TransferDevice(StorageRequest request)
		{
			await _http.PostAsJsonAsync($"{BaseUrl}/transfer", request);
		}
	}

	public class StorageRequest
	{
		public string device_id { get; set; }
		public string room_to { get; set; }
		public string date { get; set; } // yyyy-MM-dd
	}
}