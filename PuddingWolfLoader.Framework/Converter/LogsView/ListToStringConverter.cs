using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.View;
using PuddingWolfLoader.Framework.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static PuddingWolfLoader.SDK.Model.LogModel;

namespace PuddingWolfLoader.Framework.Converter
{
    class ListWhereConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return (values[0] as IEnumerable<PudLog>).Where(x => x.Type == ((PudLogType)values[1]));
            else
                return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
