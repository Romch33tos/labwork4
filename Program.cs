using System;

class Program
{
  static void Main(string[] args)
  {
    TextFile currentFile = new TextFile();
    Caretaker caretaker = new Caretaker();
    TextFileSearcher searcher = new TextFileSearcher();

    while (true)
    {
      Console.WriteLine("Меню:");
      Console.WriteLine("1. Создать новый файл");
      Console.WriteLine("2. Редактировать файл");
      Console.WriteLine("3. Сохранить файл");
      Console.WriteLine("4. Загрузить файл");
      Console.WriteLine("5. Поиск файлов по ключевым словам");
      Console.WriteLine("6. Отменить изменения");
      Console.WriteLine("0. Выйти");
      Console.Write("Введите номер команды: ");

      var choice = Console.ReadLine();
      switch (choice)
      {
        case "1":
          Console.Write("\nВведите имя файла: ");
          currentFile.FileName = Console.ReadLine();
          caretaker.Save(new TextFile { FileName = currentFile.FileName, Content = currentFile.Content });
          Console.WriteLine("Файл создан.");
          break;

        case "2":
          Console.Write("\nВведите содержимое файла: ");
          currentFile.Content = Console.ReadLine();
          caretaker.Save(new TextFile { FileName = currentFile.FileName, Content = currentFile.Content });
          Console.WriteLine("Файл изменен.");
          break;

        case "3":
          Console.Write("\nВведите имя файла для сохранения: ");
          currentFile.SerializeToXml(Console.ReadLine());
          Console.WriteLine("Файл сохранен.");
          break;

        case "4":
          Console.Write("\nВведите имя загружаемого файла: ");
          currentFile = TextFile.DeserializeFromXml(Console.ReadLine());
          Console.WriteLine("Содержимое загруженного файла: " + currentFile.Content);
          break;

        case "5":
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
          break;

        case "6":
          var previousFile = caretaker.Undo();
          if (previousFile != null)
          {
            currentFile = previousFile;
            Console.WriteLine("\nБыла произведена отмена. Содержимое файла: " + currentFile.Content);
          }
          else
          {
            Console.WriteLine("\nНет изменений для отмены.");
          }
          break;

        case "0":
          return;

        default:
          Console.WriteLine("\nНеправильный номер команды. Попробуйте снова.");
          break;
      }
    }
  }
}