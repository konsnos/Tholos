using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UVRPN.Events;
using UVRPN.Core;
using static System.Runtime.CompilerServices.RuntimeHelpers;

//VRPN Input interface class
//This provides the input for the Main Joypad or the Button Seats  
public class JoystickVRPN : MonoBehaviour
{
    //Create a custom struct and apply [Serializable] attribute to it
    [Serializable]
    public struct JoystickInfo
    {
        //name of the vrpn device
        public string tracker;
        //number of butotn on this device
        public bool[] buttons;
        //number of joystick axis on the device
        public double[] axis;
    }

    [Serializable]
    public struct DomeInfo
    {
        //if active Dome seats will be interfaces
        public bool isActive;
        //the tracker names of the seats without the number usuaally "joystick"
        public string joystickDeviceNames;
        //number of seats
        public int deviceRange;
        //number of button in each seat
        public int buttonsAmount;
        //number of joystick axis in each seat
        public int axisAmount;
    }

    //The client that poolas the server
    public VRPN_Manager vrpnManager;

    //Array holding all the joysticks
    public JoystickInfo[] joystickArray;

    //If active then this holds all the info for the vrpn inputs of the dome Seats
    //and the above joystickArray is populated automatically be the parameters of the struct
    [Header("For Dome")]
    public DomeInfo DomeSeats;

    //if true the device states are printed in the log
    public bool debugLog = false;

    //Unity events you can hook into for getting the states. You can also poll the joystickArray manually if you want
    [Header("Events")]
    [Tooltip("This is triggered when a button is changed.")]
    public ButtonJoystickVRPNEvent OnButtonChange = new ButtonJoystickVRPNEvent();
    [Tooltip("This is triggered when a axis is changed.")]
    public AnalogJoystickVRPNEvent OnAxisChange = new AnalogJoystickVRPNEvent();

    public void Start()
    {
        //If this is for the dome seats populate array automatically else its done manually
        if (DomeSeats.isActive)
        {
            joystickArray = new JoystickInfo[DomeSeats.deviceRange];

            for (int i = 0; i < DomeSeats.deviceRange; i++)
            {
                joystickArray[i].tracker = string.Format("{0}{1}", DomeSeats.joystickDeviceNames, i);

                if (DomeSeats.buttonsAmount > 0)
                    joystickArray[i].buttons = new bool[DomeSeats.buttonsAmount];

                if (DomeSeats.axisAmount > 0)
                    joystickArray[i].axis = new double[DomeSeats.axisAmount];
            }
        }

        //on debugging add custom event who print to the console
        if (debugLog)
        {
            OnButtonChange.AddListener((int joystickNr, int buttonNr, bool state) => print("Joystick" + joystickNr + " Button " + buttonNr + (state ? " Pressed" : " Released")));
            OnAxisChange.AddListener((int joystickNr, int axisNr, double value) => print("Joystick" + joystickNr + " Axis " + axisNr + "Value: " + value));
        }
    }

    public void Update()
    {
        UpdateEditor();

        UpdateDevice();
    }

    private void UpdateDevice()
    {
        if (Application.isEditor) return;

        //Update each device of joystickArray each frame
        for (int joyIndex = 0; joyIndex < joystickArray.Length; joyIndex++)
        {
            //get a joystick device
            JoystickInfo js = joystickArray[joyIndex];

            //update buttons if there are any specified
            if (js.buttons != null)
            {
                for (int i = 0; i < js.buttons.Length; i++)
                {
                    //Get state of a button from server
                    bool newState = vrpnManager.IsButtonPressed(js.tracker, i);

                    //if there was a change fire an event which has as info the number of the device in the array, the number of button on the device, the state
                    if ((js.buttons[i] != newState) && OnButtonChange != null)
                        OnButtonChange.Invoke(joyIndex, i, newState);

                    js.buttons[i] = newState;
                }
            }

            //update the joystick axis, each stick has 2 axis a joystick can have multiple sticks
            if (js.axis != null)
            {
                for (int i = 0; i < js.axis.Length; i++)
                {
                    //Get state os axis form server
                    double newValue = vrpnManager.GetAnalog(js.tracker, i);

                    //if the axis is not idle fire an event which ahs as info the number of the device in the array, the number of axis on the device, the value (-1,1 ususally)
                    double diff = js.axis[i] - newValue;

                    if ((diff >= float.Epsilon || diff <= -float.Epsilon) && OnAxisChange != null)
                        OnAxisChange.Invoke(joyIndex, i, newValue);

                    js.axis[i] = newValue;
                }
            }
        }
    }

    private void UpdateEditor()
    {
        if (!Application.isEditor) return;

        var topLeftPressed = Input.GetKey(KeyCode.Keypad7);
        var topRightPressed = Input.GetKey(KeyCode.Keypad8);
        var bottomLeftPressed = Input.GetKey(KeyCode.Keypad4);
        var bottomrightPressed = Input.GetKey(KeyCode.Keypad5);

        int startingIndex = (int)KeyCode.Alpha0;

        for (int i = startingIndex; i < startingIndex + 10; i++)
        {
            int index = i - startingIndex;

            var keyCode = (KeyCode)i;
            if (Input.GetKeyDown(keyCode))
            {
                if(topLeftPressed) OnButtonChange.Invoke(index, 0, true);
                if (topRightPressed) OnButtonChange.Invoke(index, 1, true);
                if (bottomLeftPressed) OnButtonChange.Invoke(index, 2, true);
                if (bottomrightPressed) OnButtonChange.Invoke(index, 3, true);
            }

            if (Input.GetKeyUp(keyCode))
            {
                OnButtonChange.Invoke(index, 0, false);
                if (topLeftPressed) OnButtonChange.Invoke(index, 0, false);
                if (topRightPressed) OnButtonChange.Invoke(index, 1, false);
                if (bottomLeftPressed) OnButtonChange.Invoke(index, 2, false);
                if (bottomrightPressed) OnButtonChange.Invoke(index, 3, false);
            }
        }
    }
}
