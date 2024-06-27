using UnityEngine;

namespace Colonizer
{
    [System.Serializable]
    public class ObjectDefinition : MonoBehaviour
    {
        public enum Type
        {
            Unit, Building
        }

        public Type type;
    }
}
