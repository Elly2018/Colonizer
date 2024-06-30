using UnityEngine;

namespace Colonizer
{
    public interface IPropertyBankBase
    {
        PropertyBase<T> Get<T>(string label);
        PropertyBase<bool> GetBool(string label);
        PropertyBase<string> GetString(string label);
        PropertyBase<int> GetInt(string label);
        PropertyBase<float> GetFloat(string label);
        PropertyBase<Vector2> GetVector2(string label);
        PropertyBase<Vector3> GetVector3(string label);

        void Append(PropertyHeader header);
        void Append(PropertyHeader[] header);
        bool LabelExist(PropertyType type, string label);
    }
}
