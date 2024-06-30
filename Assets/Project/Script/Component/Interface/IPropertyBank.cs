namespace Colonizer
{
    public interface IPropertyBank : IPropertyBankBase
    {
        void Append(BoolProperty v);
        void Append(BoolProperty[] v);
        void Append(StringProperty v);
        void Append(StringProperty[] v);
        void Append(IntProperty v);
        void Append(IntProperty[] v);
        void Append(FloatProperty v);
        void Append(FloatProperty[] v);
        void Append(Vector2Property v);
        void Append(Vector2Property[] v);
        void Append(Vector3Property v);
        void Append(Vector3Property[] v);
    }
}
