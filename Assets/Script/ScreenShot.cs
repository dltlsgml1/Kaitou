//********************************************
// Screenshot class ver1.0
//  complete_date_ver1.0 : 2018/06/04_ikeda
//********************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private string projectName;     // プロジェクトの名前(読み込み参照用)
    private string prefabName;      // プレハブオブジェクト名(ベース名)
    private string childPrefabName; // クリアイメージのオブジェクト名
    private string fileName;        // スクリーンショットイメージの名前
    private bool isRunning;         // コルーチン用

    // 初期化
    public void Init(string projectname, string prefabname, string childprefabname, string filename)
    {
        projectName = projectname;

        prefabName = prefabname;

        childPrefabName = childprefabname;

        fileName = filename;

        isRunning = false;
    }

    // スクリーンショットの生成
    public IEnumerator CreateClearImage(int id)
    {
        if (isRunning)
        {
            yield return null;
        }
        isRunning = true;

        // 前のスクリーンショットの削除
        yield return StartCoroutine(DeleteScreenshot(id));

        // スクリーンショットの作成
        yield return StartCoroutine(CreateScreenshot(id));

        isRunning = false;
    }

    // スクリーンショットの削除
    private IEnumerator DeleteScreenshot(int id)
    {
        string fileId = IdToString(id);

        // ファイルがあるかどうか
        if (System.IO.File.Exists(projectName + "_Data/" + fileName + fileId + ".png") == true)
        {
            // ファイル削除
            System.IO.File.Delete(projectName + "_Data/" + fileName + fileId + ".png.meta");
            while (System.IO.File.Exists(projectName + "_Data/" + fileName + fileId + ".png.meta") == true)
            {
                yield return null;
            }
            System.IO.File.Delete(projectName + "_Data/" + fileName + fileId + ".png");
            while (System.IO.File.Exists(projectName + "_Data/" + fileName + fileId + ".png") == true)
            {
                yield return null;
            }
        }

    }

    // スクリーンショットの作成
    private IEnumerator CreateScreenshot(int id)
    {
        string fileId = IdToString(id);

        // スクリーンショットを撮る
        ScreenCapture.CaptureScreenshot(fileName + fileId + ".png");

        // スクリーンショット生成まで待つ
        while (System.IO.File.Exists(fileName + fileId + ".png") == false)
        {
            yield return null;
        }

    }

    // マテリアルの変更
    public bool SearchToSetClearImage(int id)
    {
        string fileId = IdToString(id);
        MeshRenderer renderer;
        Color color;
        string path = "StagePrefab/" + prefabName + fileId + "(Clone)/" + childPrefabName;

        if (System.IO.File.Exists(projectName + "_Data/" + fileName + fileId + ".png") == true)
        {
            // ①．ファイル => バイナリ変換
            byte[] image = System.IO.File.ReadAllBytes(projectName + "_Data/" + fileName + fileId + ".png");

            // ②．受け入れ用Texture2D作成
            Texture2D tex = new Texture2D(0, 0);

            // ③．バイナリ => Texture変換
            tex.LoadImage(image);

            // ④．Texture2Dをマテリアルに指定
            //MeshRenderer renderer = GameObject.Find(prefabName + fileId).GetComponent<MeshRenderer>();
            renderer = GameObject.Find(path).GetComponent<MeshRenderer>();
            renderer.materials[0].mainTexture = tex;

            color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            renderer.material.color = color;

            return true;
        }

        renderer = GameObject.Find(path).GetComponent<MeshRenderer>();
        renderer.materials[0].mainTexture = null;

        color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
        renderer.material.color = color;
        
        return false;
    }
    
    // IDの「0」付与対応用
    private string IdToString(int id)
    {
        string str;

        if(id >= 0 && id <= 9)
        {
            str = "0" + id;
        }
        else
        {
            str = "" + id;
        }

        return str;
    }
}
