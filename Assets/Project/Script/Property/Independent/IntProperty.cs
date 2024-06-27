namespace Colonizer
{
    /// <summary>
    /// Independent int property <br />
    /// This will usually store in the <seealso cref="PropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct IntProperty: PropertyBase<int>
    {
        public IntProperty(string label, string description, int value)
        {
            Label = label;
            Description = description;
            Value = value;
        }

        public string Label { get; }
        public string Description { get; }
        public int Value { get; set; }
    }
}
