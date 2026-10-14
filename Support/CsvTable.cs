using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicExercises.Support;

public static class CsvTable
{
    public static List<Dictionary<string, string>> Read(string relativePath)
    {
        var path = Path.Combine(AppContext.BaseDirectory, relativePath);
        var lines = File.ReadAllLines(path).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        var headers = lines[0].Split(',').Select(h => h.Trim()).ToArray();
        return lines.Skip(1).Select(line =>
        {
            var cells = line.Split(',');
            return headers.Select((h, i) => (h, v: i < cells.Length ? cells[i].Trim() : ""))
                          .ToDictionary(x => x.h, x => x.v);
        }).ToList();
    }
}