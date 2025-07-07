using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MobileApp.Models;

public class MaintainService
{
	private readonly HttpClient _httpClient;

	public MaintainService()
	{
		_httpClient = new HttpClient();
		_httpClient.BaseAddress = new Uri("http://10.0.2.2:44346/"); // Android emulator
	}

	public async Task<List<Maintain>> GetMaintainsAsync()
	{
		try
		{
			var response = await _httpClient.GetAsync("api/maintainapi");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<List<Maintain>>();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("❌ Lỗi gọi API: " + ex.Message);
		}

		return new List<Maintain>();
	}
	public async Task<bool> CreateMaintainAsync(Maintain maintain)
	{
		try
		{
			var response = await _httpClient.PostAsJsonAsync("api/maintainapi", maintain);
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
