using System.Collections;
using UnityEngine;

namespace Colonizer
{
    public class ModelLoader : MonoBehaviour
    {
        [SerializeField] UnitSpawner unit_spawner;

        private void Awake()
        {
            StartCoroutine(Load());
        }

        IEnumerator Load()
        {
            UnitSpawner.CreateUnit(
                new UnitBlueprint()
                {
                    ModelPath = "UnitTest.gltf"
                },
                Vector3.zero,
                new PropertyContent[0],
                (go) =>
                {
                    go.name = "Test Name";
                }
            );
            yield return new WaitForSeconds(2f);
            UnitSpawner.CreateUnit(
                new UnitBlueprint()
                {
                    ModelPath = "UnitTest.gltf"
                },
                Vector3.one,
                new PropertyContent[0],
                (go) =>
                {
                    go.name = "Test Name 2";
                }
            );

            UnitSpawner.CreateUnit(
                new UnitBlueprint()
                {
                    ModelPath = "UnitTest.gltf"
                },
                Vector3.one * 2f,
                new PropertyContent[0],
                (go) =>
                {
                    go.name = "Test Name 3";
                }
            );
        }
    }
}
