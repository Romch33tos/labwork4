using System.Collections.Generic;
using System.IO;

public class TextFileSearcher
{
  public List<string> SearchFiles(string directory, string keyword)
  {
    List<string> foundFiles = new List<string>();
    var files = Directory.GetFiles(directory, "*.*");

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