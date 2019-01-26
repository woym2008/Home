using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour {

    bool m_bEnable = false;

    public Transform m_FirePoint;
    public GameObject m_Gun;

    public float m_MaxGunAngle;
    public float m_MinGunAngle;

    public float m_CurGunAngle = 0;
    public float m_CurAngleSpeed = 20;
    float AddAngleValue = 20.0f;
    float ReduceAngleValue = -20.0f;

	// Use this for initialization
	void Start () {
        m_bEnable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(m_bEnable)
        {
            m_CurGunAngle += Time.deltaTime * m_CurAngleSpeed;
            if(m_CurGunAngle > m_MaxGunAngle)
            {
                m_CurGunAngle = m_MaxGunAngle;
                m_CurAngleSpeed = ReduceAngleValue;
            }
            if (m_CurGunAngle < m_MinGunAngle)
            {
                m_CurGunAngle = m_MinGunAngle;
                m_CurAngleSpeed = AddAngleValue;
            }

            m_Gun.transform.localEulerAngles = new Vector3(
                m_Gun.transform.localEulerAngles.x,
                m_Gun.transform.localEulerAngles.y,
                m_CurGunAngle
            );
        }
	}

    public void EnableEmitter()
    {
        m_bEnable = true;

        m_CurGunAngle = (m_MaxGunAngle + m_MinGunAngle) * 0.5f;
    }

    public void Fire(Enemy pEnemy)
    {
        pEnemy.transform.position = this.transform.position;
        pEnemy.SetTarget(m_FirePoint.position);

        pEnemy.Fly();
    }
}
