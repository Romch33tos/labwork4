using System;

public class Memento
{
  public TextFile State { get; }

  public Memento(TextFile state)
  {
    State = state;
  }
}

public class Caretaker
{
  private readonly Stack<Memento> _mementos = new Stack<Memento>();

  public void Save(TextFile file)
  {
    _mementos.Push(new Memento(file));
  }

  public TextFile Undo()
  {
    if (_mementos.Count > 0)
    {
      return _mementos.Pop().State;
    }
    return null;
  }
}