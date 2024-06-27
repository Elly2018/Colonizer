namespace Colonizer
{
    /// <summary>
    /// Independent string property <br />
    /// This will usually store in the <seealso cref="PropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct StringProperty : PropertyBase<string>
    {
        public StringProperty(string label, string description, string value)
        {
            Label = label;
            Description = description;
            Value = value;
        }

        public string Label { get; }
        public string Description { get; }
        public string Value { get; set; }
    }
}
