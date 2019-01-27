using UnityEngine;
using System.Collections;
using Common.Fsm;

public class GameState_Ready : FSMState<GameManager>
{
    const float WaitTime = 3.0f;
    float m_Waittime = 3.0f;
	protected internal override void OnInit(IFSM<GameManager> fsm)
	{
        base.OnInit(fsm);
	}

	protected internal override void OnEnter(IFSM<GameManager> fsm)
	{
        base.OnEnter(fsm);

        m_Waittime = WaitTime;

        fsm.Owner.m_RunningTime = 0;
        //Show UI
	}

	protected internal override void OnExit(IFSM<GameManager> fsm, bool isShutdown)
	{
        base.OnExit(fsm, isShutdown);


        PlayerManager.m_Instance.CreatePlayers();

        EnemyManager.m_Instance.InitEnemyManager();

        //Close UI
	}

	protected internal override void OnUpdate(IFSM<GameManager> fsm, float elapseSeconds, float realElapseSeconds)
	{
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        m_Waittime -= elapseSeconds;

        if(m_Waittime <= 0)
        {
            ChangeState<GameState_Running>(fsm);
        }
	}

	protected internal override void OnDestroy(IFSM<GameManager> fsm)
	{
        base.OnDestroy(fsm);
	}
}
