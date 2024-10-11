using CsvHelper.Configuration;
using ShellCodingTask3.Models;
using ShellCodingTask3.Services;

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
