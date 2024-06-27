namespace Colonizer
{
    /// <summary>
    /// Independent bool property <br />
    /// This will usually store in the <seealso cref="PropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct BoolProperty: PropertyBase<bool>
    {
        public BoolProperty(string label, string description, bool value)
        {
            Label = label;
            Description = description;
            Value = value;
        }

        public string Label { get; }
        public string Description { get; }
        public bool Value { set; get; }
    }
}
