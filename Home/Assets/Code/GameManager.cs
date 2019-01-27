using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Fsm;

public class GameManager : MonoBehaviour {
    public enum GameEventState{
        ToHome = 1,
        ToHuman =2,
        ToMain = 3,
        ResetGame = 4,
    }

    public static GameManager m_Instance;

    FSM<GameManager> m_FSM;

    public int m_Score;

    public float m_RunningTime;
	// Use this for initialization
	void Start () {
        m_Instance = this;
        m_FSM = new FSM<GameManager>("GameManager", this,
                                     new GameState_Ready(),
                                     new GameState_Running(),
                                     new GameState_Home(),
                                     new GameState_Over()
                                    );

        m_FSM.Start<GameState_Ready>();
        //------------------------------------
        //ui 绑定分数m_Score
	}
	
	// Update is called once per frame
	void Update () {
        m_FSM.Update(Time.deltaTime, Time.deltaTime);

        //test
        if(Input.GetKeyDown(KeyCode.J))
        {
            //m_FSM.FireEvent(null, (int)GameEventState.ToHuman);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

	private void OnDestroy()
	{
        if(!m_FSM.IsDestroyed)
        {
            m_FSM.Shutdown();
        }
	}
    //--------------------------------------------------------------------
    public void BecameHome()
    {
        m_FSM.FireEvent(null, (int)GameEventState.ToHome);
    }

    public void GameOver()
    {
        m_FSM.ChangeState<GameState_Over>();
    }
    //--------------------------------------------------------------------
    public void AddScore(int score)
    {
        m_Score += score;

        MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Coin);
    }

    public int GetScore()
    {
        return m_Score;
    }
}
