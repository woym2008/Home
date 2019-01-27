using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    static public PlayerManager m_Instance;
    //--------------------------------------
    List<PlayerSpawer> m_Nests;

    List<PlayerBase> m_Players;
    HomeObject m_Home;
    PlayerLighting m_Lighting;

    public GameObject HomePrefab;
    public GameObject LightingPrefab;

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
        //临时这样写 应该融进整体架构 统一间隔时间
        GameUpdate(Time.deltaTime);
	}

    public void GameUpdate(float dt)
    {
        if(m_Players == null)
        {
            return;
        }

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
        if((diecount > 0))
        {
            if(m_Lighting.gameObject.activeSelf)
                m_Lighting.gameObject.SetActive(false);
        }
        else
        {
            if(!m_bCanBecameHome && m_Lighting.gameObject.activeSelf)
            {
                m_Lighting.gameObject.SetActive(false);
            }
            else if (m_bCanBecameHome && !m_Lighting.gameObject.activeSelf)
            {
                m_Lighting.gameObject.SetActive(true);
            }
        }
        if(diecount == m_Players.Count)
        {
            GameManager.m_Instance.GameOver();
        }

        m_Home.GameUpdate(dt);


        float dis = (m_Players[1].transform.position - m_Players[0].transform.position).magnitude;
        m_Lighting.UpdateAlpha(dis);
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

        m_Lighting = (Instantiate(LightingPrefab) as GameObject).gameObject.GetComponent<PlayerLighting>();
        m_Lighting.target = m_Players[1].transform;
        m_Lighting.start = m_Players[0].transform;
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
            m_Lighting.gameObject.SetActive(false);
            m_bCanBecameHome = false;
        }
    }

    public void HomeToHuman()
    {
        m_Home.StopShow();
        for (int i = 0; i < m_Players.Count; ++i)
        {
            m_Players[i].GotoWork();
            m_Players[i].CreateChild();
        }
        m_Lighting.gameObject.SetActive(true);
        m_bCanBecameHome = false;
    }
    //----------------------------------------
}
