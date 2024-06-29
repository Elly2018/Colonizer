using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// Independent vector3 property <br />
    /// This will usually store in the <seealso cref="PropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct Vector3Property : PropertyBase<Vector3>
    {
        public Vector3Property(string label, string description, Vector3 value)
        {
            Label = label;
            Description = description;
            Value = value;
        }

        public string Label { get; }
        public string Description { get; }
        public Vector3 Value { get; set; }
    }
}
