namespace BluePrism.TechTest
{
    public interface IDocumentWriter<T>
    {
        string GetText(T doc);
    }
}