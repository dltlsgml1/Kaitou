using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScreenShot : MonoBehaviour {

    public string savePath = "Assets/Resources/StageSelect/SS_Canvas/ClearStageSS/";
    //public string savePath = "";

    public string fileName = "clear_stage";
    public int count;

    bool isRunning = false;

	// Use this for initialization
	void Start () {

        count = 0;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;

            //CreateClearImage();
            //LoadClearImageToMaterial("StageSelect/SS_Canvas/ClearStageSS/");

            StartCoroutine("CreateClearImage");

        }

    }

    IEnumerator CreateClearImage()
    {
        // 並行しない用処理
        if (isRunning)
        {
            yield return null;
        }
        isRunning = true;

        // 保存先
        var texturePath = savePath + fileName + count + ".PNG";

        // 前のスクリーンショットを削除
        while(System.IO.File.Exists(texturePath))
        {
            System.IO.File.Delete(texturePath);
        }

        // スクリーンショット作成
        ScreenCapture.CaptureScreenshot(texturePath);

        yield return new WaitForEndOfFrame(); 

        // ファイルの生成確認
        while(!System.IO.File.Exists(texturePath))
        {
            Debug.Log("not yet SS");
            yield return null;
        }
        Debug.Log("createSS: " + fileName + count);

        // unityのアセットフォルダの更新
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.ImportAsset(texturePath);

        yield return new WaitForEndOfFrame();

        // マテリアルの更新
        LoadClearImageToMaterial("StageSelect/SS_Canvas/ClearStageSS/");

        // 終了処理
        isRunning = false;
    }

    void LoadClearImageToMaterial(string path)
    {
        // テクスチャロード
        Texture2D clearTex = Resources.Load(path + fileName + count) as Texture2D;

        // セットしたいオブジェクトにロードしたテクスチャをセット
        GameObject obj = GameObject.Find(fileName + count);
        obj.GetComponent<Renderer>().material.SetTexture("_MainTex", clearTex);

    }

}
