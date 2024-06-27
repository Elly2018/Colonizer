namespace Colonizer
{
    /// <summary>
    /// Dependent string property <br />
    /// This will usually store in the <seealso cref="DependentPropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct StringPropertyDependent : PropertyBase<string>
    {
        public StringPropertyDependent(string label, string description, System.Func<string> value)
        {
            Label = label;
            Description = description;
            getter = value;
        }

        public string Label { get; }
        public string Description { get; }
        public string Value { set { } get { return getter(); } }
        System.Func<string> getter { get; }
    }
}
