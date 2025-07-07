using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using MobileApp.Models;
using System.Net.Http.Json;

namespace MobileApp.ServiceAPI
{
	public class DeviceService
	{
		private readonly HttpClient _httpClient;

		public DeviceService()
		{
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("http://10.0.2.2:44346/"); 
		}

		public async Task<List<MedDevice>> GetDevicesAsync()
		{
			var response = await _httpClient.GetAsync("api/DeviceApi");
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				Console.WriteLine("[DEBUG] JSON: " + json);
				return JsonConvert.DeserializeObject<List<MedDevice>>(json);
			}

			return new List<MedDevice>();
		}

		public async Task<bool> AddDeviceAsync(MedDevice device)
		{
			try
			{
				var http = new HttpClient
				{
            BaseAddress = new Uri("http://10.0.2.2:44346/")
	
				};

				// Gửi POST request dưới dạng JSON
				var response = await http.PostAsJsonAsync("api/DeviceApi", device);

				// Đọc nội dung phản hồi để log (nếu cần)
				var content = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("✅ Thêm thiết bị thành công");
					return true;
				}
				else
				{
					Console.WriteLine($"❌ Thêm thiết bị thất bại: {response.StatusCode}");
					Console.WriteLine("📥 Phản hồi từ server: " + content);
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("❌ Lỗi khi gọi API thêm thiết bị: " + ex.Message);
				return false;
			}
		}

	}

}