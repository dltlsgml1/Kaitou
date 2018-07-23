using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public static bool InputFlag = true;

    public static bool GetButton(string key)
    {
        if (InputFlag)
        {
            if (Input.GetButton(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public static bool GetButtonDown(string key)
    {
        if (InputFlag)
        {
            if (Input.GetButtonDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
