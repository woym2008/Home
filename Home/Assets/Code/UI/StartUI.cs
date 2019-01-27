using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MusicManager.GetInstance().FindController();
        MusicManager.GetInstance().BGMCtrl.PlayBGM("battle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Start);
        SceneManager.LoadScene("MainGame");
    }
}
