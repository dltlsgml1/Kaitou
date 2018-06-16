using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject ScriptObject;
    // Use this for initialization
    void Start()
    {
        ScriptObject.GetComponent<GameMain>().TutorialFlg = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        ScriptObject.GetComponent<GameMain>().TutorialFlg = false;
    }
}
