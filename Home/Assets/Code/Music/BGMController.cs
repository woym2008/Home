using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMController : MonoBehaviour {

	public	AudioSource m_BGMSource;        
	//public	AudioSource m_BGMBossSource;   

	private AudioClip m_CurBGMClip;
	private string m_CurMusicName = "";

	private List<AudioSource> m_DynamicEfxSourceS = new List<AudioSource>();
	
	void Awake()
	{
		m_BGMSource = this.gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		//m_BGMSource = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_DynamicEfxSourceS.Count > 0)
		{
			List<AudioSource> delObjs = new List<AudioSource> ();
			foreach( AudioSource pSource in m_DynamicEfxSourceS)
			{
				if(pSource != null && pSource.isPlaying == false)
				{
					delObjs.Add(pSource);
				}
			}

			foreach(AudioSource delsou in delObjs)
			{
				m_DynamicEfxSourceS.Remove(delsou);
				GameObject.Destroy(delsou.gameObject);
			}
			delObjs.Clear();
		}
	}
	
	public void  PlayBGM(string fileName)
	{
		if(m_BGMSource == null)
		{
			return;
		}
		if (!fileName.Equals(m_CurMusicName))
		{
            object test = Resources.Load("Sound/BGM/" + fileName);
			m_CurBGMClip = Resources.Load("Sound/BGM/"+fileName) as AudioClip;
			m_BGMSource.clip = m_CurBGMClip;
			m_BGMSource.loop = true;
			m_BGMSource.Play();
			m_CurMusicName = fileName;
		}
		
		Debug.Log("AudioManager::Play()");
	}
	
	public void StopBGM()
	{
		if(m_BGMSource == null)
		{
			return;
		}

		m_BGMSource.Stop();
		m_CurMusicName = "";
		Debug.Log("Stop bm_PlayClipkground music");
	}
	
	public void PauseBGM()
	{
        if(m_BGMSource == null)
        {
            return;
        }
		m_BGMSource.Pause();
	}

	public void ResumeBGM()
	{
        if (m_BGMSource == null)
        {
            return;
        }
        m_BGMSource.UnPause();
	}

	void PlayDynamicEfx(string name)
	{
		AudioClip dClip = Resources.Load("Sound/BGM/"+name) as AudioClip;
		if(dClip != null)
		{
			Vector3 sourceObjPos = new Vector3();

			if(Camera.main != null)
			{
				sourceObjPos = Camera.main.gameObject.transform.position;
			}

			GameObject pSourceObj = new GameObject("tempSource");
			pSourceObj.transform.position = sourceObjPos;
			AudioSource pSource = pSourceObj.AddComponent<AudioSource>();

			pSource.PlayOneShot(dClip);

			m_DynamicEfxSourceS.Add(pSource);
		}
	}
}
