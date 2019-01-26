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

	private void Awake()
	{
        m_Instance = this;
	}

	// Use this for initialization
	void Start () {
        FindObjects();
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
        for (int i = 0; i < m_Players.Count;++i)
        {
            m_Players[i].GameUpdate(dt);
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

    public void BecameHome()
    {
        
    }

    public void HomeToHuman()
    {
        ;
    }
    //----------------------------------------
}
