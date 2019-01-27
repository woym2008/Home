using UnityEngine;
using System.Collections;

public class Player_Rect : PlayerBase
{
    override protected void InputUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            AddDir(Dir.Up);
        }

        if (Input.GetKey(KeyCode.S))
        {
            AddDir(Dir.Down);
        }

        if (Input.GetKey(KeyCode.A))
        {
            AddDir(Dir.Left);
        }

        if (Input.GetKey(KeyCode.D))
        {
            AddDir(Dir.Right);
        }

        //test
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateChild();
        }
    }

    public override void SayHiOtherPlayer(Collider2D collision)
    {
        base.SayHiOtherPlayer(collision);
        Player_Trangle trangle = collision.gameObject.GetComponent<Player_Trangle>();
        if (trangle != null)
        {
            PlayerManager.m_Instance.BecameHome();
        }
    }
}
