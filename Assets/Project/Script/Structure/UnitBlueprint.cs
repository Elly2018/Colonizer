namespace Colonizer
{
    [System.Serializable]
    public struct UnitBlueprint
    {
        public string UnitID;
        public string Profile;
        public string ModelPath;
        public PropertyHeader[] ExtraProperty;
        public PropertyContentGroup Assign;
        public UnitBehaviour Behaviour;
    }
}
