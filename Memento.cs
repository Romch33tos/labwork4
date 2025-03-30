using System;

public class TextMemento
{
  public TextFile State { get; }

  public TextMemento(TextFile state)
  {
    State = state;
  }
}

public class TextHistory
{
  private readonly Stack<TextMemento> _TextMementos = new Stack<TextMemento>();

  public void Save(TextFile file)
  {
    _TextMementos.Push(new TextMemento(file));
  }

  public TextFile Undo()
  {
    if (_TextMementos.Count > 0)
    {
      return _TextMementos.Pop().State;
    }
    return null;
  }
}