using System;
using UnityEngine;

public class PotatoHandler : MonoBehaviour
{
    [SerializeField] private JoystickVRPN _navigatorVRPN;

    [SerializeField] private Vector3 startPosition;

    private void OnEnable()
    {
        transform.localPosition = startPosition;

        _navigatorVRPN.OnAxisChange.AddListener(OnAxisChange);
    }

    private void OnDisable()
    {
        _navigatorVRPN.OnAxisChange.RemoveListener(OnAxisChange);
    }

    private void OnAxisChange(int arg0, int arg1, double arg2)
    {
        throw new NotImplementedException();
    }
}
