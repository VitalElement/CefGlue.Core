using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using Polly;
using NuGet;

public static class Utils
{
    public static bool MoveFolderContents(string SourcePath, string DestinationPath)
    {
        SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
        DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
        
        try
        {
            if (System.IO.Directory.Exists(SourcePath))
            {
                if (System.IO.Directory.Exists(DestinationPath) == false)
                {
                    System.IO.Directory.CreateDirectory(DestinationPath);
                }
         
                foreach (string files in System.IO.Directory.GetFiles(SourcePath))
                {
                    FileInfo fileInfo = new FileInfo(files);
                    fileInfo.MoveTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name));
                }
         
                foreach (string drs in System.IO.Directory.GetDirectories(SourcePath))
                {
                    System.IO.DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                    if (MoveFolderContents(drs, DestinationPath + directoryInfo.Name) == false)
                    {
                    return false;
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}

public class ArchiveDownloadInfo
{
    public string URL {get;set;}
    public string Name {get;set;}
    public FilePath DestinationFile {get; set;}
    public string Format {get; set;}
    public Action<DirectoryPath, ArchiveDownloadInfo> PostExtract {get; set;}
}

public class ToolchainDownloadInfo
{
    private DirectoryPath _artifactsDir;

    public ToolchainDownloadInfo(DirectoryPath artifactsDir)
    {
        _artifactsDir = artifactsDir;
        Downloads = new List<ArchiveDownloadInfo>();
    }

    public DirectoryPath BaseDir {get { return _artifactsDir.Combine(RID); } }

    public DirectoryPath ZipDir {get { return _artifactsDir.Combine("zip").Combine(RID); } }

    public string RID {get; set;}
    public List<ArchiveDownloadInfo> Downloads {get; set;}    
}
