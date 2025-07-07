using System.Globalization;

namespace MobileApp.Converters
{ 
public class DateFormatConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is DateTime date)
			return date.ToString("dd/MM/yyyy");

		return "";
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		=> throw new NotImplementedException();
}
}