using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;

namespace PMS.Services.Common;

public class SqlFileProvider : EmbeddedFileProvider
{
    private static readonly SqlFileProvider Default = new();
    private readonly String _basePath;

    public static String GetQuery([CallerMemberName] String queryName = "", [CallerFilePath] String path = "", String folder = "")
    {
        return Default.Get(queryName, path, folder);
    }

    public SqlFileProvider([CallerFilePath] String basePath = "") : base(typeof(SqlFileProvider).Assembly)
    {
        _basePath = Path.GetDirectoryName(basePath) ?? String.Empty;
    }

    public String Get([CallerMemberName] String queryName = "", [CallerFilePath] String path = "", String folder = "")
    {
        String fileDirectory = (Path.GetDirectoryName(path) ?? String.Empty) + (!String.IsNullOrWhiteSpace(folder) ? $"\\{folder}" : "");
        String prefix = MakeRelativePath(_basePath, fileDirectory).Replace('\\', '.');
        String fileName = $"{prefix}.{queryName}.sql";

        using Stream stream = Default.GetFileInfo(fileName).CreateReadStream();
        using StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    // https://stackoverflow.com/a/340454
    public static String MakeRelativePath(String fromPath, String toPath)
    {
        if (String.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
        if (String.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

        Uri fromUri = new Uri(fromPath);
        Uri toUri = new Uri(toPath);

        if (fromUri.Scheme != toUri.Scheme) return toPath;  // path can't be made relative.

        Uri relativeUri = fromUri.MakeRelativeUri(toUri);
        String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

        return relativePath;
    }
}
