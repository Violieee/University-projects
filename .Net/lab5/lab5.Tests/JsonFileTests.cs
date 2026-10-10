using lab5.Core.FileStorage;
using lab5.Core.Models;
using lab5.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab5.Tests;

[TestClass]
public class JsonFileTests
{
    [TestMethod]
    public async Task SaveAndLoadAsync_RestoresDepositors()
    {
        string path = Path.Combine(Path.GetTempPath(), $"lab5-{Guid.NewGuid():N}.json");
        try
        {
            var saved = new Bank(new JsonFileWriter(), new JsonFileReader(), path);
            saved.AddDepositor(new Depositor("Олена Коваль", "UA12345", 1234.56m, DateTime.Now.Year));
            await saved.SaveAsync();
            Assert.IsTrue(File.Exists(path));

            var loaded = new Bank(new JsonFileWriter(), new JsonFileReader(), path);
            await loaded.LoadAsync();
            Assert.AreEqual(1, loaded.GetAllDepositors().Count);
            Assert.AreEqual("Олена Коваль", loaded.Deposits[0].FullName);
            Assert.AreEqual(1234.56m, loaded.Deposits[0].Amount);
        }
        finally
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }

    [TestMethod]
    public async Task SaveWhileFiltered_StillWritesAllDepositors()
    {
        string path = Path.Combine(Path.GetTempPath(), $"lab5-{Guid.NewGuid():N}.json");
        try
        {
            var bank = new Bank(new JsonFileWriter(), new JsonFileReader(), path);
            int year = DateTime.Now.Year;
            bank.AddDepositor(new Depositor("А", "UA1", 100m, year));
            bank.AddDepositor(new Depositor("Б", "UA2", 200m, year - 1));
            bank.FilterByYear(year);
            await bank.SaveAsync();
            var result = await new JsonFileReader().LoadAsync<Depositor>(path);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
        finally
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }

    [TestMethod]
    public async Task LoadAsync_MissingFile_ReturnsNull()
    {
        string path = Path.Combine(Path.GetTempPath(), $"missing-{Guid.NewGuid():N}.json");
        var loaded = await new JsonFileReader().LoadAsync<Depositor>(path);
        Assert.IsNull(loaded);
    }

    [TestMethod]
    public async Task LoadAsync_InvalidJson_Throws()
    {
        string path = Path.Combine(Path.GetTempPath(), $"lab5-broken-{Guid.NewGuid():N}.json");
        try
        {
            await File.WriteAllTextAsync(path, "not JSON");
            await Assert.ThrowsExceptionAsync<System.Text.Json.JsonException>(async () =>
            {
                await new JsonFileReader().LoadAsync<Depositor>(path);
            });
        }
        finally
        {
            if (File.Exists(path)) File.Delete(path);
        }
    }
}
