namespace Colonizer
{
    /// <summary>
    /// Independent float property <br />
    /// This will usually store in the <seealso cref="PropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct FloatProperty: PropertyBase<float>
    {
        public FloatProperty(string label, string description, float value)
        {
            Label = label;
            Description = description;
            Value = value;
        }

        public string Label { get; }
        public string Description { get; }
        public float Value { get; set; }
    }
}
