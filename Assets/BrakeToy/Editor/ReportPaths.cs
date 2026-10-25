using System.IO;
using UnityEngine;

namespace BrakeToy.Editor
{
    // Editor outputs live outside Assets so Unity does not import generated evidence.
    internal static class ReportPaths
    {
        internal static string Report(string filename) => Resolve("", filename);
        internal static string Visual(string filename) => Resolve("Visuals", filename);
        internal static string Test(string filename) => Resolve("Tests", filename);

        private static string Resolve(string folder, string filename)
        {
            string directory = Path.GetFullPath(
                Path.Combine(Application.dataPath, "..", "Reports", folder));
            Directory.CreateDirectory(directory);
            return Path.Combine(directory, Path.GetFileName(filename));
        }
    }
}
