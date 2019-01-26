using UnityEngine;
using System.Collections;
using Common.Fsm;

public class GameState_Home : FSMState<GameManager>
{
    float m_StateTime = 0;

    protected internal override void OnInit(IFSM<GameManager> fsm)
    {
        base.OnInit(fsm);
    }

    protected internal override void OnEnter(IFSM<GameManager> fsm)
    {
        base.OnEnter(fsm);

        //Play Effect

        //Play Sound

        m_StateTime = GameConfig.HomeLifeTile;
    }

    protected internal override void OnExit(IFSM<GameManager> fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);

        PlayerManager.m_Instance.HomeToHuman();

        //Play Effect

        //Stop Sound
    }

    protected internal override void OnUpdate(IFSM<GameManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        m_StateTime -= elapseSeconds;
        if (m_StateTime <= 0)
        {
            ChangeState<GameState_Running>(fsm);
        }
    }

    protected internal override void OnDestroy(IFSM<GameManager> fsm)
    {
        
        base.OnDestroy(fsm);
    }
}