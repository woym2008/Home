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

    void Start()
    {
        m_Speed = GameConfig.BulletBeginSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_CanFly) return;
        transform.position = transform.position + (new Vector3(mV.x, mV.y, 0) * m_Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (m_CanFly == false) return;

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("CollideBullet");
            Stop();

            DestroySelf();
        }

        if (other != null && other.gameObject != null &&
           other.gameObject.tag == "Home")
        {
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
        m_Speed += GameConfig.TimeAddSpeed;

        float fFlag = -1.0f;
        switch(other.name)
        {
            case "Up":
                {
                    float lAngle = Vector3.Angle(transform.up, Vector3.right);
                    transform.Rotate(Vector3.forward * 2.0f * lAngle * fFlag);
                }
                break;
            case "Down":
                {
                    float lAngle = Vector3.Angle(transform.up, Vector3.right);
                    transform.Rotate(Vector3.forward * 2.0f * lAngle);

                }
                break;
            case "Left":
                {
                    float lAngle = Vector3.Angle(transform.up, Vector3.up);
                    transform.Rotate(Vector3.forward * 2.0f * lAngle * fFlag);

                }
                break;
            case "Right":
                {
                    float lAngle = Vector3.Angle(transform.up, Vector3.up);
                    transform.Rotate(Vector3.forward * 2.0f * lAngle);

                }
                break;
        }
    }

	private void OnEnable()
	{
        m_CanFly = false;
	}

	public void DestroySelf()
    {
        BulletFactory.ReleaseEnemy(this);
    }
}
