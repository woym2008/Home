using UnityEngine;
using System.Collections;

public abstract class Player_Little : MonoBehaviour
{
    public Transform ParentPoint;

    public Transform TailPoint;

    protected Player_Little m_Child;

    float m_smooth = 2.0f;
    float m_RotateSpeed = 5.0f;

	private void Awake()
	{
        m_Child = null;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        if(ParentPoint != null)
        {
            //rot
            Vector2 targetDir = ParentPoint.position - this.gameObject.transform.position;

            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //move
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, ParentPoint.position,Time.deltaTime * m_smooth);
        }
	}

    public void CreateChild(Player_Little pPlayer)
    {
        if(m_Child != null)
        {
            m_Child.CreateChild(pPlayer);
        }
        else
        {
            m_Child = pPlayer;
            m_Child.ParentPoint = this.TailPoint;
            m_Child.transform.position = this.TailPoint.position;

        }
    }

    public void SetChildActive(bool bset)
    {
        if (m_Child != null)
        {
            m_Child.SetChildActive(bset);
        }
        this.gameObject.SetActive(bset);
    }
}
