using CsvHelper.Configuration;
using ShellCodingTask.Services;

namespace ShellCodingTask.Models
{
    public class TouData
    {
        public string MeterCode { get; set; }
        public string Serial { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public string Quality { get; set; }
        public string Stream { get; set; }
        public string DataType { get; set; }
        public double Energy { get; set; }
        public string Units { get; set; }
    }
    public class TouDataMap : ClassMap<TouData>
    {
        public TouDataMap()
        {
            Map(m => m.MeterCode).Name("MeterCode");
            Map(m => m.Serial).Name("Serial");
            Map(m => m.PlantCode).Name("PlantCode");
            //Map(m => m.DateTime)
            //                .Name("DateTime")
            //                .TypeConverterOption.Format("dd/MM/yyyy HH:mm:ss");
            Map(m => m.DateTime).TypeConverter<DateTimeConverter>();
            Map(m => m.Stream).Name("Stream");
            Map(m => m.DataType).Name("DataType");
            Map(m => m.Energy).Name("Energy");
            Map(m => m.Units).Name("Units");
        }
    }
}
