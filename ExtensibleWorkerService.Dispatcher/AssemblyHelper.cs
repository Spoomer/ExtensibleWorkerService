using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ExtensibleWorkerService.Dispatcher;

internal static class AssemblyHelper
{
    private static Assembly[] _loadedAssemblies = Array.Empty<Assembly>();

    public static IList<Assembly> GetLoadedAssemblies(params string[] assemblyFolders)
    {
        if (_loadedAssemblies.Length > 0)
        {
            return _loadedAssemblies;
        }

        LoadAssemblies(assemblyFolders);
        return _loadedAssemblies;
    }

    private static void LoadAssemblies(string[] assemblyFolders)
    {
        HashSet<Assembly> loadedAssemblies = new();

        List<string> assembliesToBeLoaded = new();


        foreach (var assemblyFolder in assemblyFolders)
        {
            string[] assemblyPaths = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory,assemblyFolder), "*.dll");
            assembliesToBeLoaded.AddRange(assemblyPaths);
        }

        foreach (string path in assembliesToBeLoaded)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(path);
                loadedAssemblies.Add(assembly);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        _loadedAssemblies = loadedAssemblies.ToArray();
    }
}