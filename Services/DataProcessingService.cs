using CsvHelper;
using ShellCodingTask.Models;
using System.Globalization;

namespace ShellCodingTask.Services
{
    public class DataProcessingService
    {
        public async Task<(double Min, double Max, double Median)> ProcessData(Stream stream, string date, string meter, string dataType)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // Read the header row
            csv.Read();
            csv.ReadHeader();
            // Get all header names
            var headerNames = csv.HeaderRecord;
            if (csv.HeaderRecord.Contains("Data Value"))
            {
                csv.Context.RegisterClassMap<LpDataMap>();
                var records = csv.GetRecords<LpData>().ToList();
                DateTime userDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                // Filter records based on the parameters
                var filteredRecords = records
                    .Where(r => r.DateTime.Date == userDate.Date && r.MeterPointCode == meter && r.DataType == dataType)
                    .ToList();
                var dataValues = records.Select(r => r.DataValue).ToList();
                return (dataValues.Min(), dataValues.Max(), CalculateMedian(dataValues));

            }
            else if (csv.HeaderRecord.Contains("Energy"))
            {
                csv.Context.RegisterClassMap<TouDataMap>();
                var records = csv.GetRecords<TouData>().ToList();
                DateTime userDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                var filteredRecords = records
                    .Where(r => r.DateTime.Date == userDate.Date && r.MeterCode == meter && r.DataType == dataType)
                    .ToList();
                var energyValues = filteredRecords.Select(r => r.Energy).ToList();

                // Calculate statistics
                return (energyValues.Min(), energyValues.Max(), CalculateMedian(energyValues));
            }
            else
            {
                throw new ArgumentException("Invalid file type. The file must contain either 'Data Value' or 'Energy' column.");
            }

           
        }
        private double CalculateMedian(List<double> values)
        {
            var sorted = values.OrderBy(n => n).ToList();
            int count = sorted.Count;
            if (count % 2 == 0)
                return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
            return sorted[count / 2];
        }
    }
}
