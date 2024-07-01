using UnityEngine;

namespace Colonizer
{
    [AddComponentMenu("Colonizer/Game System Register")]
    [RequireComponent(typeof(ModuleLoader))]
    public class GameSystemRegister : MonoBehaviour
    {
        [SerializeField] ObjectBlueprint Objects = new ObjectBlueprint();
        [SerializeField] UnitBlueprint[] Units = new UnitBlueprint[0];
        [SerializeField] BuildingBlueprint[] Buildings = new BuildingBlueprint[0];

        IModuleLoader Loader = null;

        private void Awake()
        {
            Loader = GetComponent<ModuleLoader>();
        }
    }
}
