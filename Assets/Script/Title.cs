using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    public GameObject tt_Faield;
    private StageSelectFade cs_failed;

    public bool EnterFlg = false;

	// Use this for initialization
	void Start () {
        EnterFlg = false;
        cs_failed = tt_Faield.GetComponent<StageSelectFade>();
        Sound.LoadBgm("bgm_title", Sound.SearchFilename(Sound.eSoundFilename.TT_TitleBgm));
        Sound.LoadSe("se_ttenter", Sound.SearchFilename(Sound.eSoundFilename.TT_Enter));
        Sound.PlayBgm("bgm_title");

        // デバッグ用　セーブファイル削除
        if (Debug.isDebugBuild)
        {
            GameObject.Find("SaveData").GetComponent<ExportCsvScript>().DeleteFile();
        }

        // セーブデータ初期化
        GameObject.Find("SaveData").GetComponent<ExportCsvScript>().Init(DefineScript.MAX_STAGE);
    }
	
	// Update is called once per frame
	void Update () {

        if (cs_failed.FadeInFlag == false)
        {
            if (Controller.GetButtonDown("AButton"))
            {
                if (EnterFlg == false)
                {
                    EnterFlg = true;
                    cs_failed.FadeOutFlag = true;
                    Sound.PlaySe("se_ttenter", 7);
                }
            }
        }


        if (cs_failed.FadeOutFlag == false && EnterFlg)
        {
            Sound.StopBgm();
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        }


    }
}
