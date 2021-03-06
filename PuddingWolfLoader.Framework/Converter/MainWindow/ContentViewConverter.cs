using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.View;
using PuddingWolfLoader.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PuddingWolfLoader.Framework.Converter
{
    class ContentViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                ViewContainer.GetViewInstance<DashboardView>();
            }  
            return ViewContainer.GetViewInstance((value as ViewTab).type);

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
