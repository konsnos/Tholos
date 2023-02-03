using System;
using UnityEngine;
using UnityEngine.Events;

namespace UVRPN.Events
{
    //Event for joystick axis changes
    //parameters: int number of Joystick, int channel of axis, double axis value
    [Serializable]
    public class AnalogJoystickVRPNEvent : UnityEvent<int, int, double>
    {
    }
}