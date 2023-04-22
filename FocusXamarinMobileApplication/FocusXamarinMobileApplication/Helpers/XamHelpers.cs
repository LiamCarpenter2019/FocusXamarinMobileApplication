namespace FocusXamarinMobileApplication.Helpers;

public class BoolToValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value as bool?)
        {
            case true:
                return "Yes";
            case false:
                return "No";
            default:
                return "";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool? ret = false;

        if (value is string)
            switch (value as string)
            {
                case "Yes":
                    ret = true;
                    break;
                case "No":
                    ret = false;
                    break;
                default:
                    ret = null;
                    break;
            }

        return ret;
    }
}

public class InverseBoolConverter : IValueConverter, IMarkupExtension
{
    public object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(bool)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
        //throw new NotImplementedException();
    }
}

public class StringToBoolConverter : IValueConverter
{
    #region IValueConverter implementation

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string)
        {
            if (string.IsNullOrEmpty((string)value))
                return false;
            return true;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //throw new NotImplementedException();
        return value;
    }

    #endregion
}