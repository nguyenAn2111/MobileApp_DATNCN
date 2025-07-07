	using System;
	using System.Globalization;
	using Microsoft.Maui.Controls;

using Microsoft.Maui.Graphics;

namespace MobileApp.Converters
{

	public class StatusToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var status = value?.ToString()?.ToUpperInvariant();
			return status switch
			{
				"ĐANG BẢO TRÌ" => Color.FromArgb("#7ae582"),
				"ĐÃ XẾP LỊCH BẢO TRÌ" => Color.FromArgb("#FFEB3B"),
				"CHƯA XẾP LỊCH" => Color.FromArgb("#ea8e8c"),
				"ĐÃ XẾP LỊCH SỬA CHỮA" => Color.FromArgb("#ffd5c2"),
				"ĐANG SỬA CHỮA" => Color.FromArgb("#ade8f4"),
				"ĐÃ SỬA XONG" => Color.FromArgb("#9fffcb"),
				_ => Color.FromArgb("#25a18e")
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

}