namespace Colonizer
{
    public interface IDependentPropertyBank : IPropertyBankBase
    {
        void Append(BoolPropertyDependent v);
        void Append(BoolPropertyDependent[] v);
        void Append(StringPropertyDependent v);
        void Append(StringPropertyDependent[] v);
        void Append(IntPropertyDependent v);
        void Append(IntPropertyDependent[] v);
        void Append(FloatPropertyDependent v);
        void Append(FloatPropertyDependent[] v);
        void Append(Vector2PropertyDependent v);
        void Append(Vector2PropertyDependent[] v);
        void Append(Vector3PropertyDependent v);
        void Append(Vector3PropertyDependent[] v);
    }
}
