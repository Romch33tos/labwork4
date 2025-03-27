using System.Collections.Generic;
using System.IO;

public class FileSearcher
{
  public List<string> SearchFiles(string directory, string keyword)
  {
    List<string> foundFiles = new List<string>();
    var files = Directory.GetFiles(directory, "*.xml");

    foreach (var file in files)
    {
      var content = File.ReadAllText(file);
      if (content.Contains(keyword))
      {
        foundFiles.Add(file);
      }
    }

    return foundFiles;
  }
}