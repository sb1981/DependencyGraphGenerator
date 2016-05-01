namespace DependencyGraphGeneratorFramework
{
    public interface IDGGraphExporter
    {
        int ExportGraph(FileInfo[] fileInfos, string fileName);
        string GetName();
        string GetExtension();
    }
}
