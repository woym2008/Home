using UnityEngine;
using System.Collections;

public class BulletFactory
{
    static Recycler m_Recycler;

    static public void Init()
    {
        Clear();
        m_Recycler = new Recycler();
    }

    static public Enemy CreateEnemy(Enemy prefab, string name = "Enemy")
    {
        Enemy pEnemy = m_Recycler.Pop(name) as Enemy;
        if(pEnemy == null)
        {
            pEnemy = InstanceEnemy(prefab);
            pEnemy.name = name;
        }
        if (!pEnemy.gameObject.activeSelf)
            pEnemy.gameObject.SetActive(false);

        return pEnemy;
    }

    static public void ReleaseEnemy(Enemy pEnemy)
    {
        if(pEnemy != null)
        {
            pEnemy.gameObject.SetActive(false);
            m_Recycler.Push(pEnemy);
        }
    }

    static public void Clear()
    {
        if(m_Recycler != null)
        {
            m_Recycler.Release();
        }
    }

    private static Enemy InstanceEnemy(Enemy prefab)
    {
        Object enemyobject = Object.Instantiate(prefab.gameObject);

        Enemy newEnemy = (enemyobject as GameObject).GetComponent<Enemy>();

        if(newEnemy == null)
        {
            Debug.LogError("not create enemy sucess");
        }

        return newEnemy;
    }
}
