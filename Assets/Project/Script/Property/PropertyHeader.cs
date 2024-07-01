using NaughtyAttributes;

namespace Colonizer
{
    [System.Serializable]
    public struct PropertyHeader
    {
        public string Name;
        public string Description;
        public PropertyType DataType;
        public bool IsDependent;
        public string DependMethod;
    }
}
