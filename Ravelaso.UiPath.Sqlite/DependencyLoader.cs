using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Ravelaso.UiPath.Sqlite
{
    public static class DependencyLoader
    {
        public static void LoadInterop()
        {
            var interopFileName = "SQLite.Interop.dll";
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;
            var env = Environment.Is64BitProcess ? "x64" : "x86";

            var resource = $"{assemblyName}.Resources.{env}.{interopFileName}";

            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var interopFilePath = Path.Combine(assemblyDirectory!, interopFileName);

            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                if (stream == null)
                {
                    throw new Exception("Could not load interop dll for SQLite");
                }
                using (var fs = new FileStream(interopFilePath, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fs);
                }
            }
            // Load the DLL into the process
            LoadLibrary(interopFilePath);
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);
    }
}