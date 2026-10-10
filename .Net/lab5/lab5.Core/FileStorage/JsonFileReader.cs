using System.Text.Json;

namespace lab5.Core.FileStorage;

public class JsonFileReader : IFileLoader
{
    public Task<List<T>?> LoadAsync<T>(string filePath) => ReadFromFileAsync<T>(filePath);

    public async Task<List<T>?> ReadFromFileAsync<T>(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Потрібно вказати шлях до файлу.", nameof(filePath));
        if (!File.Exists(filePath))
            return null;

        await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read,
            FileShare.Read, bufferSize: 4096, useAsync: true);
        return await JsonSerializer.DeserializeAsync<List<T>>(stream);
    }
}
