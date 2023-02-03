/*
Simple script for moving an object along the X (sides) and Z (forward) directions with the arrow keys + Rotate with Mouse Axis
- Horizontal  : X axis : left/right 
- Vertical    : Z axis : forward/back
This script is ideal for camera controlling
*/

using UnityEngine;
using System.Collections;
//using UnityEngine.InputSystem;

//Xbox navigator for Dome.
//Attach this script to the Main Camera of display 1. Import the respective XBoxDomeNavigator.preset for the input manager.
//The Right stick does Forward motion to a specifi direction and rotation (Alternative arrow keyboard keys).
//The Joystick Left stick determines the direction (alternative keyboard keys w,s,a,d). In the editor there is a debug
//render line which shows the direction.
//The Joystick LB,RB triggers apply speedMultiplier detemined by script (Alternative keyboard c)
//The Joystick Cross does a Strafe and up/down translation (Alternative keys r,f,t,g).
//The Joystick LT,RT paddles apply a pitch (alternative keyboad keys q,e).
//The Joystick A,B,X,Y,Back,Start,Lthumb,RThumb buttons are exposed in the input manager and can trigger custom actions
//(alternative keyboard keys 1,2,3,5,6,7,8).

public class DomeVRPNNavigator : MonoBehaviour
{
    [Header ("Multiplier on LB")]
    public int speedMultiplierLB = 1;
    [Header ("Multiplier on RB")]
    public int speedMultiplierRB = 1;

    [Header("Camera Move speed")]
    public int moveSpeed = 1;
    [Header("Camera Turn speed")]
    public int rotSpeed = 1;

    float speedMultiplier = 1;

    Vector2 leftStick = Vector2.zero;
    Vector2 rightStick = Vector2.zero;
    Vector2 thumbStick = Vector2.zero;
    float pitchPaddle = 0;

    public void UpdateInputButtons(int joyNr, int buttonNr, bool val)
    {
        //Determine if a speed multiper key is pressed
        if (buttonNr == 5 )
            speedMultiplier = val ? speedMultiplierLB : 1;
        else if (buttonNr == 7)
            speedMultiplier = val ? speedMultiplierRB : 1;

        if (buttonNr == 4)
            pitchPaddle = val ? -1 : 0;
        else if (buttonNr == 6)
            pitchPaddle = val ? 1 : 0;

    }
    public void UpdateInputAxis(int joyNr, int axisNr, double value)
    {
        switch (axisNr)
        {
            case 0: leftStick.x = (float)value;
                break;
            case 1: leftStick.y = (float)value;
                break;
            case 2:
                rightStick.x = (float)value;
                break;
            case 3:
                rightStick.y = (float)value;
                break;
            case 4:
                thumbStick.x = (float)value;
                break;
            case 5:
                thumbStick.y = (float)value;
                break;
            default: break;

        } 
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 dir = transform.forward;

        //Calculate front navigation direction
        Vector3 crossDir = Vector3.Cross(dir, Vector3.up).normalized;
        dir = Quaternion.AngleAxis(leftStick.x * 60, Vector3.up) * dir;
        dir = Quaternion.AngleAxis(-leftStick.y * 60, crossDir) * dir;
        Debug.DrawRay(pos, 10 * dir, Color.yellow);

        //If digital cross is pressed then do strafe and or up movement 
        if (Mathf.Abs(thumbStick.x) > 0.2 ||
            Mathf.Abs(thumbStick.y) > 0.2)
        {
            float amountToGoUp = moveSpeed * speedMultiplier * -thumbStick.y * Time.deltaTime;
            transform.Translate(Vector3.up * amountToGoUp, Space.World);

            //apply minus because cross dir points rightwards lokat cross product above
            float amountToStrafe = -moveSpeed * speedMultiplier * thumbStick.x * Time.deltaTime;
            transform.Translate(crossDir * amountToStrafe, Space.World);
        }

        //Do actional navigation based on above computed from direction
        //-1 because the xbox controller give -1 when pressed front for the right cross pad
        float amounToTranslate = moveSpeed * speedMultiplier * rightStick.y * -1 * Time.deltaTime;
        transform.Translate(dir * amounToTranslate, Space.World);

        //Compute and apply pitch
        float amounToPitch = rotSpeed * pitchPaddle * Time.deltaTime;
        transform.Rotate(amounToPitch, 0, 0);
        
        //Compute and apply y rotation
        float amountToYaw = rotSpeed * speedMultiplier * rightStick.x * Time.deltaTime;
        transform.Rotate(0, amountToYaw, 0, Space.World);
    }

}