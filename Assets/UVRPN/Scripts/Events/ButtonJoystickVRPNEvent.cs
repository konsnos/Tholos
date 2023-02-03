using System;
using UnityEngine.Events;

namespace UVRPN.Events
{
    //Event for joystick button changes
    //parameters: int number of Joystick, int channel of button, bool button state
    [Serializable]
    public class ButtonJoystickVRPNEvent : UnityEvent<int,int,bool>
    {
    }
}