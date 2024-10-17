#define CODE1

#if CODE1
// ISP Wrong
interface IDocumentManager
{
    void EditDocument(string content);
    void ReadDocument();
}

public class Editor : IDocumentManager
{
    public void EditDocument(string content) { /* Edit logic */ }
    public void ReadDocument() { /* Read logic */ }
}

public class Reader : IDocumentManager
{
    public void EditDocument(string content)
        => throw new NotImplementedException("Read-only access");
    public void ReadDocument() { /* Read logic */ }
}

#else
// ISP Correct
interface IDocumentEditor
{
    void EditDocument(
        string content);
}

interface IDocumentReader
{
    void ReadDocument();
}

public class Editor : IDocumentEditor, IDocumentReader
{
    public void EditDocument(string content) { /* Edit logic */ }
    public void ReadDocument() { /* Read logic */ }
}

public class Reader : IDocumentReader
{
    public void ReadDocument() { /* Read logic */ }
}
#endif