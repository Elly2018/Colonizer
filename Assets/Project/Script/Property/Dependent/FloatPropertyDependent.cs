namespace Colonizer
{
    /// <summary>
    /// Dependent float property <br />
    /// This will usually store in the <seealso cref="DependentPropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct FloatPropertyDependent : PropertyBase<float>
    {
        public FloatPropertyDependent(string label, string description, System.Func<float> value)
        {
            Label = label;
            Description = description;
            getter = value;
        }

        public string Label { get; }
        public string Description { get; }
        public float Value { set { } get { return getter(); } }
        System.Func<float> getter { get; }
    }
}
