using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TodoAvalonia.Models;

namespace TodoAvalonia.Services;

public static class FileService
{
    private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Avalonia.TodoAvalonia", "db.json");

    public static async Task SaveAsync(IEnumerable<TodoItem> items)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        await using var fs = File.Create(FilePath);
        await JsonSerializer.SerializeAsync(fs, items);
    }
}