namespace Colonizer
{
    /// <summary>
    /// Dependent int property <br />
    /// This will usually store in the <seealso cref="DependentPropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct IntPropertyDependent : PropertyBase<int>
    {
        public IntPropertyDependent(string label, string description, System.Func<int> value)
        {
            Label = label;
            Description = description;
            getter = value;
        }

        public string Label { get; }
        public string Description { get; }
        public int Value { set { } get { return getter(); } }
        System.Func<int> getter { get; }
    }
}
