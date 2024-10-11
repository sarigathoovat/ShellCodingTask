using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace ShellCodingTask.Services
{
    public class DateTimeConverter : ITypeConverter
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            //  return DateTime.ParseExact(text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string[] formats = { "dd/MM/yyyy H:mm", "dd/MM/yyyy HH:mm:ss" };
            return DateTime.ParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return ((DateTime)value).ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
