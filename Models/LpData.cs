using CsvHelper.Configuration;
using ShellCodingTask.Services;
namespace ShellCodingTask.Models
{
    public class LpData
    {
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public string DataType { get; set; }
        public double DataValue { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }
    public class LpDataMap : ClassMap<LpData>
    {
        public LpDataMap()
        {
            Map(m => m.MeterPointCode).Name("MeterPoint Code");
            Map(m => m.SerialNumber).Name("Serial Number");
            Map(m => m.PlantCode).Name("Plant Code");
            // Map(m => m.DateTime).Name("Date/Time");
            Map(m => m.DateTime).Name("Date/Time").TypeConverter<DateTimeConverter>();
            Map(m => m.DataType).Name("Data Type");
            Map(m => m.DataValue).Name("Data Value");
            Map(m => m.Units).Name("Units");
            Map(m => m.Status).Name("Status");
        }
    }


}
