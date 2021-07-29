using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PuddingWolfLoader.Framework.Converter.MainWindow
{
    class ContentViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ViewContainer.GetViewInstance();

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
