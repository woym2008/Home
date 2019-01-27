using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowStartUI : MonoBehaviour {

    public GameObject StartBtn;
    public RectTransform TitleTex;

    public float m_ShowTime = 1.0f;
    float curtime;
    public Camera m_Cam;

    Color targetcolor = new Color(0, 0, 0, 1);
    Vector2 targetsize = new Vector2(1024, 1024);

    Color startcolor;
    Vector2 startsize;

	// Use this for initialization
	void Start () {
        startcolor = m_Cam.backgroundColor;
        startsize = TitleTex.sizeDelta;
        curtime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (curtime < m_ShowTime) 
        {
            curtime += Time.deltaTime;

            m_Cam.backgroundColor = Color.Lerp(startcolor,targetcolor, curtime / m_ShowTime);
            TitleTex.sizeDelta = Vector2.Lerp(startsize, targetsize,curtime/m_ShowTime);
        }
        else{
            if(!StartBtn.activeSelf)
            {
                StartBtn.SetActive(true);
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("MainGame");
            }
        }
	}
}
