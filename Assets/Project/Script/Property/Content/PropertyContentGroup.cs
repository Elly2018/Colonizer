namespace Colonizer
{
    [System.Serializable]
    public struct PropertyContentGroup
    {
        public BoolPropertyContent[] Bools;
        public StringPropertyContent[] Strings;
        public IntPropertyContent[] Ints;
        public FloatPropertyContent[] Floats;
        public Vector2PropertyContent[] Vector2s;
        public Vector3PropertyContent[] Vector3s;
    }
}
