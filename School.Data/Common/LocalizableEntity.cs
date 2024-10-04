using System.Globalization;

namespace School.Data.Common;
public class LocalizableEntity
{
    public string GetLocalized(string textEn, string textAr)
    {
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;

        return culture.TwoLetterISOLanguageName.ToLower() == "en" ? textEn : textAr;
    }
}
