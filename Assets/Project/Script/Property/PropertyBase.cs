namespace Colonizer
{
    public interface PropertyBase<T>
    {
        string Label { get; }
        string Description { get; }
        T Value { set; get; }
    }
}
