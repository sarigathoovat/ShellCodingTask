using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;
using ShellCodingTask3.Services;
namespace ShellCodingTask3.Models
{
    public class TouDataMap : ClassMap<TouData>
    {
        public TouDataMap()
        {
            Map(m => m.MeterCode).Name("MeterCode");
            Map(m => m.Serial).Name("Serial");
            Map(m => m.PlantCode).Name("PlantCode");
            //Map(m => m.DateTime)
            //                .Name("DateTime")
            //                .TypeConverterOption.Format("dd/MM/yyyy HH:mm:ss"); // Specify the format            Map(m => m.Quality).Name("Quality");
            Map(m => m.DateTime).TypeConverter<DateTimeConverter>();
            Map(m => m.Stream).Name("Stream");
            Map(m => m.DataType).Name("DataType");
            Map(m => m.Energy).Name("Energy");
            Map(m => m.Units).Name("Units");
        }
    }
}
