using UnityEngine;
using System.Collections;

public class ScreenAdaptation : MonoBehaviour
{
    int CurHeight = 0;
    Camera m_Cam;
	private void Awake()
	{
		
	}
	// Use this for initialization
	void Start()
	{
        m_Cam = Camera.main;
        if(m_Cam == null)
        {
            ;
        }
        CurHeight = Screen.height;
        m_Cam.orthographicSize = Screen.height * 0.5f * 0.01f;
	}

	// Update is called once per frame
	void Update()
	{
        if(Screen.height != CurHeight)
        {
            CurHeight = Screen.height;
            m_Cam.orthographicSize = Screen.height * 0.5f * 0.01f;
        }
	}
}
