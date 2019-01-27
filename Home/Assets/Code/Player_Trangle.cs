using UnityEngine;
using System.Collections;

public class Player_Trangle : PlayerBase
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

    }

	public override void SayHiOtherPlayer(Collider2D collision)
	{
        base.SayHiOtherPlayer(collision);
        Player_Rect rect = collision.gameObject.GetComponent<Player_Rect>();
        if(rect != null)
        {
            PlayerManager.m_Instance.BecameHome();
        }
	}
}
