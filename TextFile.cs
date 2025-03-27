using System.IO;
using System.Xml.Serialization;

[Serializable]
public class TextFile
{
  public string FileName { get; set; }
  public string Content { get; set; }

  public void SerializeToXml(string path)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
    using (StreamWriter writer = new StreamWriter(path))
    {
      serializer.Serialize(writer, this);
    }
  }

  public static TextFile DeserializeFromXml(string path)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
    using (StreamReader reader = new StreamReader(path))
    {
      return (TextFile)serializer.Deserialize(reader);
    }
  }

  public void SerializeToBinary(string path)
  {
    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
    {
      writer.Write(FileName);
      writer.Write(Content);
    }
  }

  public static TextFile DeserializeFromBinary(string path)
  {
    using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
    {
      return new TextFile
      {
        FileName = reader.ReadString(),
        Content = reader.ReadString()
      };
    }
  }
}