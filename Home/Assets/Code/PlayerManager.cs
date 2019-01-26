using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    static public PlayerManager m_Instance;
    //--------------------------------------
    List<PlayerSpawer> m_Nests;

    PlayerBase m_PlayerMan;
    PlayerBase m_PlayerWoman;
    HomeObject m_Home;

	private void Awake()
	{
        m_Instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
