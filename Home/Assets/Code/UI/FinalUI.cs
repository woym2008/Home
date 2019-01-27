using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalUI : MonoBehaviour {

    public Text TimeText;
    public Text ScoreText;

	// Use this for initialization
	void Start () {
        TimeText.text = GameDataMgr.instance.m_CurTime + " 秒";
        ScoreText.text = GameDataMgr.instance.m_CurScore + " 分";
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void GoToGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}
