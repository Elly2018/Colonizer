using UnityEngine;

namespace Colonizer
{
    [AddComponentMenu("Colonizer/Spawner/Building Spawner")]
    public class BuildingSpawner : MonoBehaviour
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
