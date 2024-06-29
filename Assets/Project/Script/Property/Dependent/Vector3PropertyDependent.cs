using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// Dependent vector3 property <br />
    /// This will usually store in the <seealso cref="DependentPropertyGroupComponent"/> <br />
    /// </summary>
    [System.Serializable]
    public struct Vector3PropertyDependent : PropertyBase<Vector3>
    {
        public Vector3PropertyDependent(string label, string description, System.Func<Vector3> value)
        {
            Label = label;
            Description = description;
            getter = value;
        }

        public string Label { get; }
        public string Description { get; }
        public Vector3 Value { set { } get { return getter(); } }
        System.Func<Vector3> getter { get; }
    }
}
