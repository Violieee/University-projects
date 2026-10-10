namespace lab5.Core.FileStorage;

public interface IFileSaver
{
    Task SaveAsync<T>(string filePath, List<T> data);
}
