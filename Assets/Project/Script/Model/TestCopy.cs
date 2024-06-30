using UnityEngine;

namespace Colonizer
{
    public class TestCopy : MonoBehaviour
    {
        private void OnEnable()
        {
            enabled = false;
            GameObject.Instantiate(gameObject);
        }
    }
}
