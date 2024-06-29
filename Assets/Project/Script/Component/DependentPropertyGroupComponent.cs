using UnityEngine;

namespace Colonizer
{
    [System.Serializable]
    public class DependentPropertyGroupComponent : MonoBehaviour
    {
        public PropertyBase<bool>[] bool_values { set; get; }
        public PropertyBase<string>[] string_values { set; get; }
        public PropertyBase<int>[] int_values { set; get; }
        public PropertyBase<float>[] float_values { set; get; }
        public PropertyBase<Vector2>[] vector2_values { set; get; }
        public PropertyBase<Vector3>[] vector3_values { set; get; }
    }
}
