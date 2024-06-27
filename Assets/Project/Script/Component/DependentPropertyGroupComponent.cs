using UnityEngine;

namespace Colonizer
{
    [System.Serializable]
    public class DependentPropertyGroupComponent : MonoBehaviour
    {
        public PropertyBase<bool>[] bool_values;
        public PropertyBase<string>[] string_values;
        public PropertyBase<int>[] int_values;
        public PropertyBase<float>[] float_values;
    }
}
