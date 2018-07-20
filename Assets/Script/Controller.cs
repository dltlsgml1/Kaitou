using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static bool GetBouttonFlag(string key, bool flag)
    {
        if (flag)
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
    public static bool GetButtonDownFlag(string key, bool flag)
    {
        if (flag)
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
