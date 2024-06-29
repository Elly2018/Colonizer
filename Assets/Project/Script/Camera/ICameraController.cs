using UnityEngine;

namespace Colonizer
{
    public interface ICameraController
    {
        float CurrentSensitivity { set; get; }
        float CurrentHeight { set; get; }
        KeyCode HeightRaiseHotKey { set; get; }
        KeyCode HeightLowerHotKey { set; get; }
    }
}
