using UnityEngine;
using System.Collections;
using Common.Fsm;

public class GameState_Running : FSMState<GameManager>
{
    protected internal override void OnInit(IFSM<GameManager> fsm)
    {
        base.OnInit(fsm);

        SubscribeEvent((int)GameManager.GameEventState.ToHome, new FsmEventHandler<GameManager>(OnBecameHome));
    }

    protected internal override void OnEnter(IFSM<GameManager> fsm)
    {
        base.OnEnter(fsm);

        PlayerManager.m_Instance.CreatePlayers();

        EnemyManager.m_Instance.InitEnemyManager();

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

    }

    protected internal override void OnDestroy(IFSM<GameManager> fsm)
    {
        base.OnDestroy(fsm);
    }

    public void OnBecameHome(IFSM<GameManager> fSM, object sender, object data)
    {
        ChangeState<GameState_Ready>(fSM);
    }
}
