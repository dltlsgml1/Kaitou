using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSfade : MonoBehaviour {

    public Vector3 CalculatePosition;

    RectTransform rect_transform;

    // Use this for initialization
    void Start () {
        rect_transform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Pause.BackStageSelect_flg)
        {
            rect_transform.localPosition = new Vector3(CalculatePosition.x, CalculatePosition.y, CalculatePosition.z);

        }
        else
        {

        }
    }
}
