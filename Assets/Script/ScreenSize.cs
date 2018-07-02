using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour {
	public int ScreenWidth;
	public int ScreenHeight;
	// Use this for initialization
	void  Awake() {
		// PC向けビルドだったらサイズ変更
		if (Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.OSXPlayer ||
			Application.platform == RuntimePlatform.LinuxPlayer )
		{
			Screen.SetResolution(ScreenWidth, ScreenHeight, false);
			Screen.fullScreen = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
