namespace Ucu.Poo.Repositories
{
    public interface IHasValue
    {
        bool HasValue(string field, string value);
    }
}