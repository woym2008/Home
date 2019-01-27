using UnityEngine;
using System.Collections;

public class HomeObject : MonoBehaviour
{
    bool m_bIsShowing = false;
	// Use this for initialization
	void Start()
	{
        m_bIsShowing = false;
	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public void GameUpdate(float dt)
    {
        ;
    }

    public void ShowTime()
    {
        m_bIsShowing = true;
        this.gameObject.SetActive(true);
        //play animation
    }

    public void StopShow()
    {
        m_bIsShowing = false;
        this.gameObject.SetActive(false);
    }

    public void GetCoin(int score)
    {      
        //if (m_bIsShowing)
        {
            //Debug.LogError("GetCoin");
            GameManager.m_Instance.AddScore(score);
        }
    }
    //
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_bIsShowing)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy pEnemy = collision.gameObject.GetComponent<Enemy>();
                GameManager.m_Instance.AddScore(pEnemy.m_Score);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_bIsShowing)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Enemy pEnemy = collision.gameObject.GetComponent<Enemy>();
                GameManager.m_Instance.AddScore(pEnemy.m_Score);
            }
        }

    }
    */
    //
}
