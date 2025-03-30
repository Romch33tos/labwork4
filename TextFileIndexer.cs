using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TextFileIndexer
{
  private Dictionary<string, List<string>> _keywordIndex = new Dictionary<string, List<string>>();

  public void IndexFiles(string directory, string filePattern = "*.txt")
  {
    _keywordIndex.Clear();
    var files = Directory.GetFiles(directory, filePattern);

    foreach (var file in files)
    {
      var content = File.ReadAllText(file);
      var words = content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (var word in words)
      {
        if (!_keywordIndex.ContainsKey(word))
        {
          _keywordIndex[word] = new List<string>();
        }

        if (!_keywordIndex[word].Contains(file))
        {
          _keywordIndex[word].Add(file);
        }
      }
    }
  }

  public List<string> SearchIndex(string keyword)
  {
    if (_keywordIndex.TryGetValue(keyword, out var files))
    {
      return files;
    }
    
    return new List<string>();
  }

  public void SaveIndex(string filePath)
  {
    using (var writer = new StreamWriter(filePath))
    {
      foreach (var entry in _keywordIndex)
      {
        writer.WriteLine($"{entry.Key}:{string.Join(",", entry.Value)}");
      }
    }
  }

  public void LoadIndex(string filePath)
  {
    _keywordIndex.Clear();
    var lines = File.ReadAllLines(filePath);

    foreach (var line in lines)
    {
      var parts = line.Split(':');
      if (parts.Length == 2)
      {
        var keyword = parts[0];
        var files = parts[1].Split(',').ToList();
        _keywordIndex[keyword] = files;
      }
    }
  }
}