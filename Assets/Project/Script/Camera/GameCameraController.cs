using System;
using UnityEngine;

namespace Colonizer
{
    /// <summary>
    /// This script will control the RTS camera behaviour <br />
    /// Outter class can access it by using the interface <seealso cref="ICameraController"/> to control with <br />
    /// It simplify for you
    /// </summary>
    [AddComponentMenu("Colonizer/Camera/Game Camera Controller")]
    public class GameCameraController : MonoBehaviour, ICameraController
    {
        [Header("Camera Setting")]
        [SerializeField] [Range(SensitivityMin, SensitivityMax)] float Sensitivity = 1f;
        [SerializeField][Range(HeightMin, HeightMax)] float Height = 1f;
        [SerializeField] KeyCode HeightRaise = KeyCode.Q;
        [SerializeField] KeyCode HeightLower = KeyCode.E;
        [Header("Camera Setup")]
        [SerializeField] LayerMask FloorMask;

        const float SensitivityMultiply = 2.0f;
        const float SensitivityMin = 0.1f;
        const float SensitivityMax = 25.0f;

        const float HeightMultiply = 5.0f;
        const float HeightMin = 3.0f;
        const float HeightMax = 25.0f;

        float HeightVelocity = 0.0f;

        public KeyCode HeightRaiseHotKey { get => HeightRaise; set => HeightRaise = value; }
        public KeyCode HeightLowerHotKey { get => HeightLower; set => HeightLower = value; }
        public float CurrentSensitivity { get => Sensitivity; set => Sensitivity = Mathf.Clamp(value, SensitivityMin, SensitivityMax); }
        public float CurrentHeight { get => Height; set => Height = Mathf.Clamp(value, HeightMin, HeightMax); }

        public static ICameraController Instance
        {
            get
            {
                if(_Instance == null && Camera.main != null)
                {
                    GameCameraController gcc = Camera.main.gameObject.AddComponent<GameCameraController>();
                    _Instance = gcc;
                }
                return _Instance;
            }
        }
        static ICameraController _Instance;

        private void Awake()
        {
            _Instance = this;
        }

        void FixedUpdate()
        {
            CameraMovement();
            CameraHeight();
            CameraHeightControl();
        }

        void CameraMovement()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) dir += Vector2.up;
            if (Input.GetKey(KeyCode.S)) dir -= Vector2.up;
            if (Input.GetKey(KeyCode.A)) dir += Vector2.left;
            if (Input.GetKey(KeyCode.D)) dir -= Vector2.left;
            transform.Translate(new Vector3(dir.x, 0, dir.y) * Time.deltaTime * SensitivityMultiply * Sensitivity, Space.World);
        }

        void CameraHeight()
        {
            Vector3 rayP = transform.position + new Vector3(0, 999f, 3f);
            Ray ray = new Ray(rayP, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 99999f, FloorMask))
            {
                float target = hit.point.y + Height;
                Vector3 b = transform.localPosition;
                transform.localPosition = new Vector3(b.x, target, b.z);
            }
        }

        void CameraHeightControl()
        {
            if (Input.GetKey(HeightRaise))
            {
                Height += HeightMultiply * Time.deltaTime;
            }
            if (Input.GetKey(HeightLower))
            {
                Height -= HeightMultiply * Time.deltaTime;
            }
            Height = Mathf.Clamp(Height, HeightMin, HeightMax);
        }
    }
}
