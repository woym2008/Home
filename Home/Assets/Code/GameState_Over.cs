using UnityEngine;
using System.Collections;
using Common.Fsm;
using UnityEngine.SceneManagement;

public class GameState_Over : FSMState<GameManager>
{
    protected internal override void OnInit(IFSM<GameManager> fsm)
    {
        base.OnInit(fsm);
    }

    protected internal override void OnEnter(IFSM<GameManager> fsm)
    {
        base.OnEnter(fsm);

        //Play UI
    }

    protected internal override void OnExit(IFSM<GameManager> fsm, bool isShutdown)
    {
        base.OnExit(fsm, isShutdown);

        //Play Effect

        //Stop Sound
    }

    protected internal override void OnUpdate(IFSM<GameManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
    }

    protected internal override void OnDestroy(IFSM<GameManager> fsm)
    {
        base.OnDestroy(fsm);
    }

    public void OnResetGame(IFSM<GameManager> fSM, object sender, object data)
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OnReturnMain(IFSM<GameManager> fSM, object sender, object data)
    {
        SceneManager.LoadScene("Entrance");
    }
}
