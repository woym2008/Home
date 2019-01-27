using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager m_Instance;

    private List<EnemyEmitter> m_Emitters;

    public Enemy m_EnemyPrefab;

    bool m_bEnableFire = false;

    float MaxCooldownTime = 1.0f;
    float m_CooldownTime = 1.0f;

    int m_CurrentEmitterIndex = 0;

	private void Awake()
	{
        m_Instance = this;
	}

	// Use this for initialization
	void Start () {
        m_bEnableFire = false;
        m_CurrentEmitterIndex = 0;
        m_bAllDie = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(m_bEnableFire)
        {
            m_CooldownTime -= Time.deltaTime;
            if(m_CooldownTime <= 0)
            {
                Fire();
                m_CooldownTime = GameConfig.AddBulletSpeed;
            }
        }

        //test
        if(Input.GetKeyDown(KeyCode.T))
        {
            BulletFactory.Init();
            FindEmitter();
            EnableEmitters();
        }
	}

    public void InitEnemyManager()
    {
        BulletFactory.Init();
        FindEmitter();
        EnableEmitters();
    }

    private void FindEmitter()
    {
        m_Emitters = new List<EnemyEmitter>();
        GameObject EmittersRoot = GameObject.Find("EnemyEmitters");
        for (int i = 0; i < EmittersRoot.transform.childCount;++i)
        {
            m_Emitters.Add(EmittersRoot.transform.GetChild(i).GetComponent<EnemyEmitter>());
        }
    }

    public void EnableEmitters()
    {
        m_bEnableFire = true;

        for (int i = 0; i < m_Emitters.Count; ++i)
        {
            m_Emitters[i].EnableEmitter();
        }
    }

    private void Fire()
    {
        if(m_CurrentEmitterIndex >= m_Emitters.Count)
        {
            m_CurrentEmitterIndex = 0;
        }

        Enemy pEnemy = BulletFactory.CreateEnemy(m_EnemyPrefab);

        m_Emitters[m_CurrentEmitterIndex].Fire(pEnemy);

        m_CurrentEmitterIndex++;
    }

    //Temp Func
    public bool m_bAllDie = false;
    public void AllDie()
    {
        m_bAllDie = true;
    }

    public void AllDieOver()
    {
        m_bAllDie = false;
    }

    public bool IsAllDie()
    {
        return m_bAllDie;
    }
}
