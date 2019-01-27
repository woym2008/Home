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

    bool m_bIsAlive = true;
    public bool IsAlive
    {
        get
        {
            return m_bIsAlive;
        }
    }

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

    bool m_bWorking = true;
   
	// Use this for initialization
	void Start () {
        m_MoveSpeed = GameConfig.PlayerMoveSpeed;
        m_Child = null;
        m_bWorking = true;
        m_bIsAlive = true;
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
        if(!m_bWorking)
        {
            return;
        }
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
            m_Child.m_PlayerMain = this;
        }
        else
        {
            m_Child.CreateChild(newChild.GetComponent<Player_Little>());
        }
    }

    public void Sleep()
    {
        m_bWorking = false;
        this.gameObject.SetActive(false);
        SetChildActive(false);
    }

    public void GotoWork()
    {
        m_bWorking = true;
        this.gameObject.SetActive(true);
        SetChildActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!m_bIsAlive)
        {
            return;
        }
        if (m_bWorking)
        {
            if (collision.gameObject.tag == "Player")
            {
                SayHiOtherPlayer(collision);
                //PlayerManager.m_Instance.BecameHome();
            }
            CheckEnemy(collision);
        }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if(!m_bIsAlive)
        {
            return;
        }
        if(m_bWorking)
        {
            if (collision.gameObject.tag == "Player")
            {         
                SayHiOtherPlayer(collision);
                //PlayerManager.m_Instance.BecameHome();
            }
            CheckEnemy(collision);
        }

	}

    public void CheckEnemy(Collider2D collision)
    {
        if (!m_bIsAlive)
        {
            return;
        }
        if (m_bWorking)
        {
            if (collision.gameObject.tag == "Player")
            {
                SayHiOtherPlayer(collision);
                //PlayerManager.m_Instance.BecameHome();
            }
            if (collision.gameObject.tag == "Enemy")
            {
                Die();
            }
        }
    }

    virtual public void SayHiOtherPlayer(Collider2D collision)
    {
        ;
    }

    public void SetChildActive(bool bset)
    {
        if (m_Child != null)
        {
            m_Child.SetChildActive(bset);
        }
    }

    protected void Die()
    {
        m_bIsAlive = false;
        m_bWorking = false;
        this.gameObject.SetActive(false);
        SetChildActive(false);
        PlayDieAnimation();
        PlayDieSound();
    }

    virtual protected void PlayDieAnimation()
    {
        ;
    }

    virtual protected void PlayDieSound()
    {
        ;
    }
}
