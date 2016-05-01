namespace DependencyGraphGeneratorFramework
{
    public interface IDGFileAnalyzer
    {
        int AnalyzeFile(ref FileInfo fi);
        string GetName();
    }
}
