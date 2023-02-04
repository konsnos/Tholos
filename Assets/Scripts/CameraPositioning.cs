using NaughtyAttributes;
using UnityEngine;

public class CameraPositioning : MonoBehaviour
{
    [Button("Position For Editor", EButtonEnableMode.Playmode)]
    private void PositionForEditor()
    {
        transform.position = new Vector3(0f, 236f, -300f);
        transform.rotation = Quaternion.Euler(44f, 0f, 0f);
    }
}
