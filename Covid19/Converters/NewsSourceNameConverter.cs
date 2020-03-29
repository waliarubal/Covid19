using Covid19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Covid19.Converters
{
    public class NewsSourceNameConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((NewsSource)value)
            {
                case NewsSource.AlJazeera:
                    return "Al Jazeera";

                case NewsSource.BBCNews:
                    return "BBC News";

                case NewsSource.CNN:
                    return "CNN";

                case NewsSource.TheNewYorkTimes:
                    return "The New York Times";

                case NewsSource.TimesOfIndia:
                    return "Times Of India";

                case NewsSource.YahooNews:
                    return "Yahoo News";

                default:
                    return default(string);
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
