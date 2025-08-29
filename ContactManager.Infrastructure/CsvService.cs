using ContactManager.Application.Contracts.Infrastructure;
using CsvHelper;
using System.Globalization;

namespace ContactManager.Infrastructure
{
    public class CsvService<T> : ICsvService<T> where T : class
    {
        public async Task<List<T>> ParseCsvAsync(Stream fileStream)
        {
            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = new List<T>();

            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                records.Add(record);
            }

            return records;
        }
    }
}