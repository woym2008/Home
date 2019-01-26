using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    static public PlayerManager m_Instance;
    //--------------------------------------
    List<PlayerSpawer> m_Nests;

    List<PlayerBase> m_Players;
    HomeObject m_Home;

    public GameObject HomePrefab;

    bool m_bCanBecameHome;

	private void Awake()
	{
        m_Instance = this;
	}

	// Use this for initialization
	void Start () {
        FindObjects();
        m_bCanBecameHome = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            //FindObjects();
            CreatePlayers();
        }
	}

    public void GameUpdate(float dt)
    {
        int diecount = 0;
        for (int i = 0; i < m_Players.Count;++i)
        {
            if(!m_Players[i].IsAlive)
            {
                diecount++;
                continue;
            }
            m_Players[i].GameUpdate(dt);
        }
        if(diecount == m_Players.Count)
        {
            GameManager.m_Instance.GameOver();
        }

        m_Home.GameUpdate(dt);
    }

    //----------------------------------------
    void FindObjects()
    {
        m_Nests = new List<PlayerSpawer>();

        GameObject NestParent = GameObject.Find("Nests");

        for (int i = 0; i < NestParent.transform.childCount;++i)
        {
            m_Nests.Add(NestParent.transform.GetChild(i).GetComponent<PlayerSpawer>());
        }
    }

    public void CreatePlayers()
    {
        m_Players = new List<PlayerBase>();
        for (int i = 0; i < m_Nests.Count;++i)
        {
            m_Players.Add(m_Nests[i].CreatePlayer());
        }

        m_Home = (Instantiate(HomePrefab) as GameObject).gameObject.GetComponent<HomeObject>();
    }

    public void SetBecameHome(bool bSet)
    {
        m_bCanBecameHome = bSet;
    }

    public void BecameHome()
    {
        if(m_bCanBecameHome)
        {
            Vector3 homeposition = Vector3.zero;
            for (int i = 0; i < m_Players.Count; ++i)
            {
                homeposition += m_Players[i].transform.position;

                m_Players[i].Sleep();
            }
            homeposition = homeposition * 0.5f;
            m_Home.transform.position = homeposition;
            m_Home.ShowTime();
            GameManager.m_Instance.BecameHome();
        }
    }

    public void HomeToHuman()
    {
        m_Home.StopShow();
        for (int i = 0; i < m_Players.Count; ++i)
        {
            m_Players[i].GotoWork();
        }
        m_bCanBecameHome = false;
    }
    //----------------------------------------
}
