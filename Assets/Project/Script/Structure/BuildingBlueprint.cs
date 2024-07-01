using NaughtyAttributes;

namespace Colonizer
{
    [System.Serializable]
    public struct BuildingBlueprint
    {
        public string Profile;
        public string ModelPath;
        [AllowNesting] public PropertyHeader[] PropertyHeaders;
        public PropertyContentGroup Assign;
    }
}
