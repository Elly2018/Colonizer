using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// Independent vector2 property <br />
    /// This will usually store in the <seealso cref="PropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct Vector2Property : PropertyBase<Vector2>
    {
        public Vector2Property(string label, string description, Vector2 value)
        {
            Label = label;
            Description = description;
            Value = value;
        }

        public string Label { get; }
        public string Description { get; }
        public Vector2 Value { get; set; }
    }
}
