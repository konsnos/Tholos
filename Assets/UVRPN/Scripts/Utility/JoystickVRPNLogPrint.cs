using UnityEngine;
using UnityEngine.UI;

public class JoystickVRPNLogPrint : MonoBehaviour
{
    [SerializeField] private bool showLog;
    [SerializeField] private JoystickVRPN _seatsVRPN;
    [SerializeField] private JoystickVRPN _navigatorVRPN;

    public Text txtUI = null;

    private void Start()
    {
        _seatsVRPN.OnButtonChange.AddListener(Testbutton);
        _navigatorVRPN.OnAxisChange.AddListener(TestAnalog);
    }

    public void Testbutton(int joyNr, int buttonNr, bool val)
    {
        if (!showLog) return;

        if (txtUI)
            txtUI.text += "Joystick" + joyNr + " Button " + buttonNr + (val ? " Pressed" : " Released") + "\n";
        else
            print("Joystick" + joyNr + " Button " + buttonNr + (val ? " Pressed" : " Released"));
    }

    public void TestAnalog(int joyNr, int axisNr, double value)
    {
        if (!showLog) return;

        if (txtUI)
            txtUI.text += "Joystick" + joyNr + " Axis " + axisNr + " Value: " + value.ToString("F2") + "\n";
        else
            print("Joystick" + joyNr + " Axis " + axisNr + " Value: " + value);
    }
}
