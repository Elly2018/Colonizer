using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// Dependent vector2 property <br />
    /// This will usually store in the <seealso cref="DependentPropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct Vector2PropertyDependent : PropertyBase<Vector2>
    {
        public Vector2PropertyDependent(string label, string description, System.Func<Vector2> value)
        {
            Label = label;
            Description = description;
            getter = value;
        }

        public string Label { get; }
        public string Description { get; }
        public Vector2 Value { set { } get { return getter(); } }
        System.Func<Vector2> getter { get; }
    }
}
