using System;
using System.Collections.Generic;

class Editor
{
    public string Content { get; set; }

    public EditorMemento CreateMemento()
    {
        return new EditorMemento(Content);
    }

    public void RestoreMemento(EditorMemento memento)
    {
        Content = memento.SavedContent;
    }

    public void ShowContent()
    {
        Console.WriteLine($"Текущий текст: {Content}");
    }
}

class EditorMemento
{
    public string SavedContent { get; }

    public EditorMemento(string content)
    {
        SavedContent = content;
    }
}

class History
{
    private Stack<EditorMemento> _mementos = new Stack<EditorMemento>();

    public void SaveState(Editor editor)
    {
        _mementos.Push(editor.CreateMemento());
    }

    public void Undo(Editor editor)
    {
        if (_mementos.Count > 0)
        {
            editor.RestoreMemento(_mementos.Pop());
        }
    }
}

class Program
{
    static void Main()
    {
        var editor = new Editor();
        var history = new History();

        editor.Content = "Первая версия текста";
        history.SaveState(editor);

        editor.Content = "Измененный текст";
        history.SaveState(editor);

        editor.ShowContent(); 

        history.Undo(editor);
        editor.ShowContent(); 
    }
}