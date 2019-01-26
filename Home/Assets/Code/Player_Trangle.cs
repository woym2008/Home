using UnityEngine;
using System.Collections;

public class Player_Trangle : PlayerBase
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
    }


}
