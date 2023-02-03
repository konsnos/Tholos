using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickVRPNLogPrint : MonoBehaviour
{
    public Text txtUI = null;

   public void Testbutton(int joyNr, int buttonNr, bool val)
    {
        if (txtUI)
            txtUI.text += "Joystick" + joyNr + " Button " + buttonNr + (val ? " Pressed" : " Released") + "\n";
        else
            print("Joystick" + joyNr + " Button " + buttonNr + (val ? " Pressed" : " Released"));
    }

    public void TestAnalog(int joyNr, int axisNr, double value)
    {
        if (txtUI)
            txtUI.text += "Joystick" + joyNr + " Axis " + axisNr + " Value: " + value.ToString("F2") + "\n";
        else
            print("Joystick" + joyNr + " Axis " + axisNr + " Value: " + value);
    }
}
