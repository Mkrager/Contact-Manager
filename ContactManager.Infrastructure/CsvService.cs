using ContactManager.Application.Contracts.Infrastructure;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace ContactManager.Infrastructure
{
    public class CsvService<T> : ICsvService<T> where T : class
    {
        public async Task<List<T>> ParseCsvAsync(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            using var csv = new CsvReader(reader, config);

            csv.Context.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[]
            {
                "dd.MM.yyyy",
                "MM/dd/yyyy",
                "yyyy-MM-dd",
                "dd/MM/yyyy",
                "d.M.yyyy",
                "M/d/yyyy"
            };

            var records = new List<T>();

            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                records.Add(record);
            }

            return records;
        }
    }
}
