namespace ContactManager.Application.Contracts.Infrastructure
{
    public interface ICsvService<T> where T : class
    {
        Task<List<T>> ParseCsvAsync(Stream fileStream);
    }
}