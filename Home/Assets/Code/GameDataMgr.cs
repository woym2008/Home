using UnityEngine;
using System.Collections;

public class GameDataMgr : XSingleton<GameDataMgr>
{
    public float m_CurTime
    {
        get
        {
            return m_RealCurTime;
        }
        set
        {
            m_RealCurTime = value;
            if(m_RealCurTime > HistoryHighTime)
            {
                HistoryHighTime = m_RealCurTime;
                PlayerPrefs.SetFloat("HistoryTime", HistoryHighTime);
            }
        }
    }
    public float m_CurScore 
    {
        get
        {
            return m_RealCurScore;
        }
        set
        {
            m_RealCurScore = value;
            if (m_RealCurScore > HistoryHighScore)
            {
                HistoryHighScore = m_RealCurScore;
                PlayerPrefs.SetFloat("HistoryScore", HistoryHighScore);
            }
        }
    }


    public float m_RealCurTime = 0;
    public float m_RealCurScore = 0;

    public float HistoryHighTime = 16.78f;
    public float HistoryHighScore = 800;

	public override void Create()
	{
        base.Create();

        HistoryHighScore = PlayerPrefs.GetFloat("HistoryScore", HistoryHighScore);
        HistoryHighTime = PlayerPrefs.GetFloat("HistoryTime", HistoryHighTime);
	}

	public override void UnInit()
	{
        base.UnInit();

        PlayerPrefs.SetFloat("HistoryTime", HistoryHighTime);
        PlayerPrefs.SetFloat("HistoryScore", HistoryHighScore);
	}
}
