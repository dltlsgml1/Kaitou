using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

    public string savePath = "Assets/Resources/StageSelect/SS_Canvas/ClearStageSS/";

    public string fileName = "clear_stage";
    public int count;

    //bool isRunning = false;

	// Use this for initialization
	void Start () {

        count = 0;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;

            CreateClearImage();
            LoadClearImageToMaterial("StageSelect/SS_Canvas/ClearStageSS/");
        }

    }

    void CreateClearImage()
    {
        var texturePath = savePath + fileName + count + ".PNG";

        ScreenCapture.CaptureScreenshot(texturePath);

        Debug.Log("createSS: " + texturePath);

    }

    void LoadClearImageToMaterial(string path)
    {
        GameObject obj = Instantiate(Resources.Load(path + "Models/" + fileName + 1)) as GameObject;
        //obj.renderer.material.mainTexture = Resources.Load("image") as Texture2D;
        obj.GetComponent<Renderer>().material.mainTexture = Resources.Load(path + fileName + count) as Texture2D;

        //Material mat = Resources.Load(path + "Material/" + fileName + count) as Material;

        //mat.mainTexture = Resources.Load(path + fileName + count) as Texture2D;
    }

}
