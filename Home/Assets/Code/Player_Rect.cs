using UnityEngine;
using System.Collections;

public class Player_Rect : PlayerBase
{
    override protected void InputUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            AddDir(Dir.Up);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            AddDir(Dir.Down);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            AddDir(Dir.Left);
        }

        if (Input.GetKey(KeyCode.RightArrow))
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
