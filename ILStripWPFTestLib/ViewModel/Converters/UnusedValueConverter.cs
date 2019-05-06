﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ILStripWPFTestLib.ViewModel.Converters
{
  class UnusedValueConverter: IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool b = (bool)value;
      return b ? "Property is Checked" : "Property is not Checked";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
