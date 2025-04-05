public class Program
{
  private static TextFile currentFile = new TextFile();
  private static TextHistory TextHistory = new TextHistory();
  private static TextFileSearcher searcher = new TextFileSearcher();
  private static TextFileIndexer indexer = new TextFileIndexer();

  static void Main(string[] args)
  {
    while (true)
    {
      ShowMenu();
      var choice = Console.ReadLine();
      HandleUserChoice(choice);
    }
  }

  private static void ShowMenu()
  {
    Console.WriteLine("Меню:");
    Console.WriteLine("1. Создать новый файл");
    Console.WriteLine("2. Редактировать файл");
    Console.WriteLine("3. Сохранить файл");
    Console.WriteLine("4. Загрузить файл");
    Console.WriteLine("5. Поиск файлов по ключевым словам");
    Console.WriteLine("6. Отменить изменения");
    Console.WriteLine("7. Индексировать файлы в директории");
    Console.WriteLine("8. Поиск по индексу");
    Console.WriteLine("9. Сохранить индекс");
    Console.WriteLine("10. Загрузить индекс");
    Console.WriteLine("0. Выйти");
    Console.Write("Введите номер команды: ");
  }

  private static void HandleUserChoice(string choice)
  {
    switch (choice)
    {
      case "1":
      {
        CreateNewFile();
        break;
      }
      case "2":
      {
        EditFile();
        break;
      }
      case "3":
      {
        SaveFile();
        break;
      }
      case "4":
      {
        LoadFile();
        break;
      }
      case "5":
      {
        SearchFiles();
        break;
      }
      case "6":
      {
        UndoChanges();
        break;
      }
      case "7":
      {
        IndexFiles();
        break;
      }
      case "8":
      {
        SearchIndex();
        break;
      }
      case "9":
      {
        SaveIndex();
        break;
      }
      case "10":
      {
        LoadIndex();
        break;
      }
      case "0":
      {
        Environment.Exit(0);
        break;
      }
      default:
      {
        Console.WriteLine("\nНеправильный номер команды. Попробуйте снова.");
        break;
      }
    }
  }

  private static void CreateNewFile()
  {
    Console.Write("\nВведите имя файла: ");
    currentFile.FileName = Console.ReadLine();
    TextHistory.Save(new TextFile { 
      FileName = currentFile.FileName, 
      Content = currentFile.Content 
    });
    Console.WriteLine("Файл создан.");
  }

  private static void EditFile()
  {
    Console.Write("\nВведите содержимое файла: ");
    currentFile.Content = Console.ReadLine();
    TextHistory.Save(new TextFile { 
      FileName = currentFile.FileName, 
      Content = currentFile.Content 
    });
    Console.WriteLine("Файл изменен.");
  }

  private static void SaveFile()
  {
    Console.Write("\nВведите имя файла для сохранения: ");
    currentFile.SerializeToXml(Console.ReadLine());
    Console.WriteLine("Файл сохранен.");
  }

  private static void LoadFile()
  {
    Console.Write("\nВведите имя загружаемого файла: ");
    currentFile = TextFile.DeserializeFromXml(Console.ReadLine());
    Console.WriteLine("Содержимое загруженного файла: " + currentFile.Content);
  }

  private static void SearchFiles()
  {
    Console.Write("\nВведите директорию для поиска: ");
    string directory = Console.ReadLine();
    Console.Write("Введите ключевое слово: ");
    string keyword = Console.ReadLine();
    
    var results = searcher.SearchFiles(directory, keyword);
    Console.WriteLine("Найденные файлы:");
    
    foreach (var result in results)
    {
      Console.WriteLine(result);
    }
  }

  private static void UndoChanges()
  {
    var previousFile = TextHistory.Undo();
    if (previousFile != null)
    {
      currentFile = previousFile;
      Console.WriteLine("\nБыла произведена отмена. Содержимое файла: " + currentFile.Content);
    }
    else
    {
      Console.WriteLine("\nНет изменений для отмены.");
    }
  }

  private static void IndexFiles()
  {
    Console.Write("\nВведите директорию для индексации: ");
    string indexDirectory = Console.ReadLine();
    Console.Write("Введите шаблон формата файла (например, *.txt): ");
    string pattern = Console.ReadLine();
    
    indexer.IndexFiles(indexDirectory, pattern);
    Console.WriteLine("Индексация завершена.");
  }

  private static void SearchIndex()
  {
    Console.Write("\nВведите ключевое слово для поиска: ");
    string searchKeyword = Console.ReadLine();
    
    var indexedResults = indexer.SearchIndex(searchKeyword);
    Console.WriteLine("Найденные файлы:");
    
    foreach (var result in indexedResults)
    {
      Console.WriteLine(result);
    }
  }

  private static void SaveIndex()
  {
    Console.Write("\nВведите путь для сохранения индекса: ");
    indexer.SaveIndex(Console.ReadLine());
    Console.WriteLine("Индекс сохранен.");
  }

  private static void LoadIndex()
  {
    Console.Write("\nВведите путь к файлу индекса: ");
    indexer.LoadIndex(Console.ReadLine());
    Console.WriteLine("Индекс загружен.");
  }
}