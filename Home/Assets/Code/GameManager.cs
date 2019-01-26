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

	// Use this for initialization
	void Start () {
        m_Instance = this;
        m_FSM = new FSM<GameManager>("GameManager", this,
                                     new GameState_Ready(),
                                     new GameState_Running()
                                    );
	}
	
	// Update is called once per frame
	void Update () {
        m_FSM.Update(Time.deltaTime, Time.deltaTime);

        //test
        if(Input.GetKeyDown(KeyCode.J))
        {
            m_FSM.FireEvent(null, (int)GameEventState.ToHuman);
        }
	}

	private void OnDestroy()
	{
        if(!m_FSM.IsDestroyed)
        {
            m_FSM.Shutdown();
        }
	}
}
