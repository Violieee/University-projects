using System.Text.Encodings.Web;
using System.Text.Json;

namespace lab5.Core.FileStorage;

public class JsonFileWriter : IFileSaver
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public Task SaveAsync<T>(string filePath, List<T> data) =>
        WriteToFileAsync(filePath, data);

    public async Task WriteToFileAsync<T>(string filePath, List<T> data)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Потрібно вказати шлях до файлу.", nameof(filePath));
        ArgumentNullException.ThrowIfNull(data);

        string? directory = Path.GetDirectoryName(Path.GetFullPath(filePath));
        if (directory is not null)
            Directory.CreateDirectory(directory);

        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write,
            FileShare.None, bufferSize: 4096, useAsync: true);
        await JsonSerializer.SerializeAsync(stream, data, Options);
    }
}
