using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private TMP_Text cameraText;

    private void Update()
    {
        cameraText.text = $"Pos: {cameraTransform.position}\nRot: {cameraTransform.rotation.eulerAngles}";
    }
}
