using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
    public GameObject tt_Faield;
    private failed cs_failed;

	// Use this for initialization
	void Start () {
        cs_failed = tt_Faield.GetComponent<failed>();
        Sound.LoadBgm("bgm_title", Sound.SearchFilename(Sound.eSoundFilename.TT_TitleBgm));
        Sound.LoadSe("se_ttenter", Sound.SearchFilename(Sound.eSoundFilename.TT_Enter));
        Sound.PlayBgm("bgm_title");

        // セーブデータ初期化
        GameObject.Find("SaveData").GetComponent<ExportCsvScript>().Init(31);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("AButton"))
        {
            cs_failed.FadeIn_On();
            Sound.PlaySe("se_ttenter", 7);

        }
        if (cs_failed.FadeInEnd)
        {
            Sound.StopBgm();     
            SceneManager.LoadScene("StageSelect", LoadSceneMode.Single);
        }

    }
}
