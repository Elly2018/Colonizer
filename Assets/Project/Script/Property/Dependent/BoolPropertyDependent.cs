namespace Colonizer
{
    /// <summary>
    /// Dependent bool property <br />
    /// This will usually store in the <seealso cref="DependentPropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct BoolPropertyDependent: PropertyBase<bool>
    {
        public BoolPropertyDependent(string label, string description, System.Func<bool> value)
        {
            Label = label;
            Description = description;
            getter = value;
        }

        public string Label { get; }
        public string Description { get; }
        public bool Value { set { } get { return getter(); } }
        System.Func<bool> getter { get; }
    }
}
