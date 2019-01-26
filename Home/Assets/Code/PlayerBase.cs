using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour {

    public enum Dir
    {
        Up = 1,
        Left = 2,
        Down = 4,
        Right = 8
    }

    int m_CurDir;

    float m_MoveSpeed = 1;

    Vector3[] Dirs =
    {
        new Vector3(0,1,0),       //up
        //new Vector2(0.5f,0.5f),   //upright
        new Vector3(1,0,0),       //right
        //new Vector2(0.5f,-0.5f),  //downright
        new Vector3(0,-1,0),      //down
        //new Vector2(-0.5f,-0.5f), //downleft
        new Vector3(-1,0,0),      //left
        //new Vector2(-0.5f,0.5f)   //upleft
    };

    //尾巴位置
    public Transform TailPoint;

    public GameObject m_ChildPrefab;
    protected Player_Little m_Child;
   
	// Use this for initialization
	void Start () {
        m_MoveSpeed = GameConfig.PlayerMoveSpeed;
        m_Child = null;

	}
	
	// Update is called once per frame
	void Update () {
        m_CurDir = 0;
        InputUpdate();
        UpdateMove();
	}

    public void GameUpdate(float dt)
    {
        ;
    }

    virtual protected void InputUpdate()
    {
        
    }

    protected void AddDir(Dir d)
    {
        m_CurDir |= (int)d;
    }
    protected bool HasDir(Dir d)
    {
        if ((m_CurDir & (int)d) != 0)
        {
            return true;
        }

        return false;
    }

    float m_RotateSpeed = 5.0f;
    virtual protected void UpdateMove()
    {
        Vector3 currentDir = Vector2.zero;

        if(HasDir(Dir.Up))
        {
            currentDir += Dirs[0];
        }
        if (HasDir(Dir.Right))
        {
            currentDir += Dirs[1];
        }
        if (HasDir(Dir.Down))
        {
            currentDir += Dirs[2];
        }
        if (HasDir(Dir.Left))
        {
            currentDir += Dirs[3];
        }
        currentDir.Normalize();

        this.transform.position = this.transform.position + currentDir * Time.deltaTime * m_MoveSpeed;
        //rot
        if(currentDir != Vector3.zero)
        {
            float angle = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    public void CreateChild()
    {
        GameObject newChild = Instantiate(m_ChildPrefab) as GameObject;

        if(m_Child == null)
        {
            m_Child = newChild.GetComponent<Player_Little>();
            m_Child.ParentPoint = TailPoint;
        }
        else
        {
            m_Child.CreateChild(newChild.GetComponent<Player_Little>());
        }
    }
}
