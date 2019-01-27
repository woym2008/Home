using UnityEngine;
using System.Collections;
using Common.Fsm;

public class GameState_Running : FSMState<GameManager>
{
    private float m_HomeCoolDownTime;



    protected internal override void OnInit(IFSM<GameManager> fsm)
    {
        base.OnInit(fsm);

        SubscribeEvent((int)GameManager.GameEventState.ToHome, new FsmEventHandler<GameManager>(OnBecameHome));
    }

    protected internal override void OnEnter(IFSM<GameManager> fsm)
    {
        base.OnEnter(fsm);

        m_HomeCoolDownTime = GameConfig.BecameHomeCooldown;
        //SoundManager
    }

    protected internal override void OnExit(IFSM<GameManager> fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);

        //Close UI
    }

    protected internal override void OnUpdate(IFSM<GameManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        if(m_HomeCoolDownTime > 0)
        {
            m_HomeCoolDownTime -= elapseSeconds;
        }
        else 
        {
            PlayerManager.m_Instance.SetBecameHome(true);
        }

        //临时这样写 应该融进整体架构 统一间隔时间
        PlayerManager.m_Instance.GameUpdate(elapseSeconds);

        fsm.Owner.m_RunningTime += elapseSeconds;
    }

    protected internal override void OnDestroy(IFSM<GameManager> fsm)
    {
        base.OnDestroy(fsm);
    }

    public void OnBecameHome(IFSM<GameManager> fSM, object sender, object data)
    {
        ChangeState<GameState_Home>(fSM);
    }
}
