using System;
using System.Globalization;
using System.Windows.Data;
using System.Reflection;
using CourseProject.Models;

namespace CourseProject.Helpers
{
    public class EnumLocalizer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());

            if (memberInfo.Length == 0) return value.ToString();

            var attribute = memberInfo[0].GetCustomAttribute<LocalizedColumnAttribute>();
            // CORRECTED: Use DisplayName instead of Name
            return attribute?.Name ?? value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}