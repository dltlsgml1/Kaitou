using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartToA : MonoBehaviour {
    public float MaxScale = 1.0f;
    public float MiniScale = 0.5f;
    public float SetTime = 2.0f;
    Vector3 NowScale;
    float time = 0f;
    float TimeCount;
    float UpSpeed;
    float DownSpeed;
    bool UpFlag = true;
    bool DownFlag = false;
	// Use this for initialization
	void Start () {
        TimeCount = MiniScale;
        UpSpeed = MaxScale / (SetTime * 60);
        DownSpeed = MiniScale / (SetTime * 60);

	}
	
	// Update is called once per frame
	void Update () {
        ChangeColor();
        ChangeScale();
    }

    void ChangeColor()
    {
        float val = Mathf.PingPong(Time.time, 1.0F);        //0～1.0の間を行き来する
        Color color = new Color(val, val, 0);    //Green + Alpha-Channel
        GetComponent<Renderer>().material.SetColor("_EmissionColor", color);　//ここで色を入れ込む。
    }

    void ChangeScale()
    {
        time = Mathf.PingPong(TimeCount, MaxScale);
        if (UpFlag)
        {
            TimeCount += UpSpeed;
        }
        if (DownFlag)
        {
            TimeCount -= DownSpeed;
        }

        float val = time;
        this.transform.localScale = new Vector3(val, val, val);

        if (UpFlag && !DownFlag&&time>MaxScale-0.1f)
        {
            UpFlag = false;
            DownFlag = true;
        }

        if (DownFlag && !UpFlag&&time<MiniScale+0.1f)
        {
            UpFlag = true;
            DownFlag = false;
        }
        
  
        

    }
}
