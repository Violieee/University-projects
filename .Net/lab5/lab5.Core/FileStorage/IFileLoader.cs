namespace lab5.Core.FileStorage;

public interface IFileLoader
{
    Task<List<T>?> LoadAsync<T>(string filePath);
}
