using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IRecyclableObject {

    public string GetRecycleType()
    {
        return "Enemy";
    }

    public void Dispose()
    {
        ;
    }
    //------------------------------------------------
    public Vector2 V
    {
        set
        {
            value.Normalize();
            mV = new Vector2(value.x, value.y);
        }
        //
        get
        {
            return mV;
        }
    }

    public void SetTarget(Vector2 target)
    {
        mV = target - new Vector2(transform.position.x, transform.position.y);
        mV.Normalize();
    }

    public float m_Speed = 1;

    protected Vector2 mV;

    public bool m_CanFly = false;

    public void Fly() { m_CanFly = true; }
    public void Stop() { m_CanFly = false; }

    public int m_Score = 100;

    string LastRebound = "";

    bool isCoin = false;
    public GameObject res_ball;
    public GameObject res_coin;

    void Start()
    {
        //m_Speed = GameConfig.BulletBeginSpeed;
        m_Speed = GameConfig.s_BulletBeginSpeed;
        isCoin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_CanFly) return;
        transform.position = transform.position + (new Vector3(mV.x, mV.y, 0) * m_Speed * Time.deltaTime);

        if(EnemyManager.m_Instance.IsHome && !isCoin)
        {
            res_ball.SetActive(false);
            res_coin.SetActive(true);
            isCoin = true;
        }
        else if(!EnemyManager.m_Instance.IsHome && isCoin)
        {
            res_ball.SetActive(true);
            res_coin.SetActive(false);
        }

        if(EnemyManager.m_Instance.IsAllDie())
        {
            DestroySelf();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_CanFly == false) return;

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Player")
        {
            //other.gameObject.SendMessage("CollideBullet");
            Stop();

            DestroySelf();
        }

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Home")
        {
            //Debug.LogError("Home");
            other.gameObject.SendMessage("GetCoin", m_Score);
            Stop();

            DestroySelf();
        }

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Frame")
        {
            Rebound(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
	{
        if (m_CanFly == false) return;

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Player")
        {
            //other.gameObject.SendMessage("CollideBullet");
            Stop();

            DestroySelf();
        }

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Home")
        {
            //Debug.LogError("Home");
            other.gameObject.SendMessage("GetCoin", m_Score);
            Stop();

            DestroySelf();
        }

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Frame")
        {
            Rebound(other);
        }
	}

	private void Rebound(Collider2D other)
    {
        float fFlag = -1.0f;
        switch(other.name)
        {
            case "Up":
                {
                    if(LastRebound == "Up")
                    {
                        return;
                    }
                    mV = Vector3.Reflect(mV, transform.up);
                    LastRebound = "Up";
                }
                break;
            case "Down":
                {
                    if (LastRebound == "Down")
                    {
                        return;
                    }
                    mV = Vector3.Reflect(mV, transform.up);
                    LastRebound = "Down";

                }
                break;
            case "Left":
                {
                    if (LastRebound == "Left")
                    {
                        return;
                    }
                    mV = Vector3.Reflect(mV, transform.right);
                    LastRebound = "Left";

                }
                break;
            case "Right":
                {
                    if (LastRebound == "Right")
                    {
                        return;
                    }
                    mV = Vector3.Reflect(mV, transform.right);
                    LastRebound = "Right";

                }
                break;
        }

        m_Speed += GameConfig.TimeAddSpeed;
    }

	private void OnEnable()
	{
        m_CanFly = false;
        res_ball.SetActive(true);
        res_coin.SetActive(false);
        m_Speed = GameConfig.s_BulletBeginSpeed;
        isCoin = false;
        LastRebound = "";
	}

	public void DestroySelf()
    {
        BulletFactory.ReleaseEnemy(this);
    }

    public void DirectDie()
    {
        Stop();
        this.DestroySelf();
    }
}
