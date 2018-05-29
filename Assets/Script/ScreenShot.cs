/*
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
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private string projectName;

    private string saveFilePath;
    private string fileName;

    private int stageMax = 3;

    private int stageId;
    private bool isRunning;

    // Use this for initialization
    void Start()
    {
        projectName = "test";
        saveFilePath = CreateSavePath(projectName);

        stageId = 0;

        isRunning = false;

        StartCoroutine(CreateScreenshot(stageId));

        for (int i = 1; i <= stageMax; i++)
        {
            SearchToSetCrearImage(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // スクリーンショット作成
            StartCoroutine(CreateScreenshot(stageId));
            // マテリアルの更新
            StartCoroutine(LoadClearImageToMaterial(stageId));
        }
        keyNum();

    }

    void keyNum()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            stageId = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stageId = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stageId = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            stageId = 3;
        }
    }

    IEnumerator CreateScreenshot(int id)
    {
        if (System.IO.File.Exists("clear_stage" + id + ".png") == true)
        {
            // ファイル削除
            System.IO.File.Delete("clear_stage" + id + ".png");
            while (System.IO.File.Exists("clear_stage" + id + ".png") == true)
            {
                yield return null;
            }
        }

        // スクリーンショットを撮る
        ScreenCapture.CaptureScreenshot("clear_stage" + id + ".png");


        while (System.IO.File.Exists("clear_stage" + id + ".png") == false)
        {
            yield return null;
        }

    }

    IEnumerator LoadClearImageToMaterial(int id)
    {
        if (isRunning)
        {
            yield return null;
        }
        isRunning = true;

        while (System.IO.File.Exists("test_Data/" + "clear_stage" + id + ".png") == false)
        {
            yield return null;
        }
        // ①．ファイル => バイナリ変換
        byte[] image = System.IO.File.ReadAllBytes("test_Data/" + "clear_stage" + id + ".png");

        // ②．受け入れ用Texture2D作成
        Texture2D tex = new Texture2D(0, 0);

        // ③．バイナリ => Texture変換
        tex.LoadImage(image);

        // ④．Texture2Dをマテリアルに指定
        MeshRenderer renderer = GameObject.Find("clear_stage" + id).GetComponent<MeshRenderer>();
        renderer.materials[0].mainTexture = tex;

        isRunning = false;
    }

    void SearchToSetCrearImage(int id)
    {
        if (System.IO.File.Exists("test_Data/" + "clear_stage" + id + ".png") == true)
        {
            // ①．ファイル => バイナリ変換
            byte[] image = System.IO.File.ReadAllBytes("test_Data/" + "clear_stage" + id + ".png");

            // ②．受け入れ用Texture2D作成
            Texture2D tex = new Texture2D(0, 0);

            // ③．バイナリ => Texture変換
            tex.LoadImage(image);

            // ④．Texture2Dをマテリアルに指定
            MeshRenderer renderer = GameObject.Find("clear_stage" + id).GetComponent<MeshRenderer>();
            renderer.materials[0].mainTexture = tex;

        }
    }

    // 保存先
    private string CreateSavePath(string projectname)
    {
        return projectname + "_Data/";
    }

    // ステージIDの名前作成(クリア用)
    private string CreateClearFilename()
    {
        return "clear_stage";
    }

    // ステージIDのファイル名作成(クリア用)
    private string CreatePngFilename(int id)
    {
        return CreateClearFilename() + id + ".png";
    }
}
