﻿using UnityEngine;
using System.Collections;

public class MusicManager
{
	private static MusicManager mInst;

	public SoundController SFXCtrl = null;

	public BGMController BGMCtrl = null;
	
	private MusicManager () {}
	
	static public MusicManager GetInstance () {
		if (mInst == null) {
			mInst = new MusicManager();
		}
		return mInst;
	}

    public void FindController()
    {
        BGMCtrl = GameObject.Find("BGM").GetComponent<BGMController>();
        SFXCtrl = GameObject.Find("SFX").GetComponent<SoundController>();
    }

	public float m_SoundVolum = 1.0f;
}