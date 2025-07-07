using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MobileApp.Models;

public class RepairService
{
	private readonly HttpClient _httpClient;

	public RepairService()
	{
		_httpClient = new HttpClient();
		_httpClient.BaseAddress = new Uri("http://10.0.2.2:44346/"); // Android emulator
	}

	public async Task<List<Repair>> GetRepairsAsync()
	{
		try
		{
			var response = await _httpClient.GetAsync("api/repairapi");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<List<Repair>>();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Lỗi gọi API: " + ex.Message);
		}

		return new List<Repair>();
	}
	public async Task<bool> CreateRepairAsync(Repair repair)
	{
		try
		{
			var response = await _httpClient.PostAsJsonAsync("api/repairapi", repair);
			if (response.IsSuccessStatusCode)
				return true;

			var error = await response.Content.ReadAsStringAsync();
			Console.WriteLine("❌ API Lỗi: " + error);
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Exception khi gửi yêu cầu: " + ex.Message);
		}
		return false;
	}
}
