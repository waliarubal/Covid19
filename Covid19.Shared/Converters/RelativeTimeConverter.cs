using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19.Shared.Converters
{
    public class RelativeTimeConverter : IValueConverter, IMarkupExtension
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateToConvert = (DateTime)value;

            var timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - dateToConvert.Ticks);
            var delta = Math.Abs(timeSpan.TotalSeconds);

            if (delta < 1 * MINUTE)
                return timeSpan.Seconds == 1 ? "a second ago" : $"{timeSpan.Seconds} seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return $"{timeSpan.Minutes} minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return delta == 1 ? "an hour ago" : $"{timeSpan.Hours} hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return $"{timeSpan.Days} days ago";

            if (delta < 12 * MONTH)
            {
                var months = Math.Floor((double)timeSpan.Days / 30);
                return months <= 1 ? "a month ago" : $"{months} months ago";
            }
            else
            {
                var years = Math.Floor((double)timeSpan.Days / 365);
                return years <= 1 ? "an year ago" : $"{years} years ago";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
