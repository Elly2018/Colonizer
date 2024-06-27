using UnityEngine;

namespace Colonizer
{
    [AddComponentMenu("Colonizer/Spawner/Unit Spawner")]
    public class UnitSpawner : MonoBehaviour
    {
        public void CreateUnit()
        {
            GameObject go = new GameObject();
            DependentPropertyGroupComponent dpgc = go.AddComponent<DependentPropertyGroupComponent>();
            PropertyGroupComponent pgc = go.AddComponent<PropertyGroupComponent>();
            ObjectDefinition od = go.AddComponent<ObjectDefinition>();
        }
    }
}
