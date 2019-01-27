using UnityEngine;
using System.Collections;
using Common.Fsm;

public class GameState_Home : FSMState<GameManager>
{
    float m_StateTime = 0;
    int waitcount = 0;

    protected internal override void OnInit(IFSM<GameManager> fsm)
    {
        base.OnInit(fsm);
    }

    protected internal override void OnEnter(IFSM<GameManager> fsm)
    {
        base.OnEnter(fsm);
        //Enable Shake
        CamShake.m_Instance.setShakeTime = GameConfig.HomeLifeTile;
        CamShake.m_Instance.shake();
        waitcount = 2;

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
            if(waitcount > 0)
            {
                EnemyManager.m_Instance.AllDie();
                waitcount--;
            }
            else
            {
                EnemyManager.m_Instance.AllDieOver();
                Debug.Log("End Home");
                ChangeState<GameState_Running>(fsm);
            }
        }

        fsm.Owner.m_RunningTime += elapseSeconds;

        //临时这样写 应该融进整体架构 统一间隔时间
        PlayerManager.m_Instance.GameUpdate(elapseSeconds);


    }

    protected internal override void OnDestroy(IFSM<GameManager> fsm)
    {
        
        base.OnDestroy(fsm);
    }
}