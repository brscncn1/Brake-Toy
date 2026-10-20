using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BrakeToy.Tests;

internal static class Program
{
    private static int Main(string[] args)
    {
        int failed = 0;
        var results = new List<object>();
        foreach (var check in CoreChecks.Create())
        {
            string error = null;
            try { check.Value(); }
            catch (Exception exception) { error = exception.Message; failed++; }
            Console.WriteLine((error == null ? "PASS " : "FAIL ") + check.Key + (error == null ? "" : ": " + error));
            results.Add(new { name = check.Key, passed = error == null, error });
        }
        Console.WriteLine((results.Count - failed) + "/" + results.Count + " checks passed");
        if (args.Length > 0)
            File.WriteAllText(args[0], JsonSerializer.Serialize(new
            {
                executedAtUtc = DateTime.UtcNow,
                runtime = Environment.Version.ToString(),
                checks = results.Count,
                failures = failed,
                randomizedCollisionCases = 10000,
                unityEditorExecuted = false,
                androidDeviceExecuted = false,
                results
            }, new JsonSerializerOptions { WriteIndented = true }));
        return failed == 0 ? 0 : 1;
    }
}
