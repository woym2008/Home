using UnityEngine;
using System.Collections;

public enum SoundType
{
	Bullet = 0,
	Coin,
	Crash,
	Start,
	GameOver
}

public class SoundController : MonoBehaviour {

	private AudioSource m_AudioSource;
	      
	private AudioClip m_CurSoundClip;

	public AudioClip BulletSound;
	public float m_BulletDealyTime = 0.0f;

	public AudioClip m_CoinSound;

    public AudioClip m_StartSound;

    public AudioClip m_CrashSound;

    public AudioClip m_GameOverSound;


	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
		m_AudioSource = this.gameObject.AddComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		m_AudioSource = this.gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PlaySound(SoundType type)
	{
		float delaytime = 0.0f;
		switch(type)
		{
            case SoundType.Bullet:
			{
				delaytime = 0;
			}
				break;
			case SoundType.Coin:
			{
				delaytime = 0;
			}
				break;
			case SoundType.Start:
			{
				delaytime = 0;
			}
				break;
			case SoundType.GameOver:
			{
				delaytime = 0;
			}
				break;
			case SoundType.Crash:
			{
				delaytime = 0;
			}
			
				break;
		}

		if(delaytime == 0.0f)
		{
			PlaySoundDirect(type);
		}
		else
		{
			StartCoroutine(waitPlaySound(type, delaytime));
		}	
	}

	IEnumerator waitPlaySound(SoundType type, float delay)
	{
		yield return new WaitForSeconds(delay);
		PlaySoundDirect(type);
	}

	public void PlaySoundDirect(SoundType type)
	{
		switch(type)
		{
            case SoundType.Bullet:
				{
                    if(BulletSound != null)
					{
                        m_AudioSource.PlayOneShot(BulletSound,MusicManager.GetInstance().m_SoundVolum);
							//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
					}
				}
				break;
            case SoundType.Crash:
		{
                    if(m_CrashSound != null)
			{
                        m_AudioSource.PlayOneShot(m_CrashSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
            case SoundType.Start:
		{
			if(m_StartSound != null)
			{
                        m_AudioSource.PlayOneShot(m_StartSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
            case SoundType.Coin:
		{
			if(m_CoinSound != null)
			{
                        m_AudioSource.PlayOneShot(m_CoinSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
            case SoundType.GameOver:
		{
                    if(m_GameOverSound != null)
			{
                        m_AudioSource.PlayOneShot(m_GameOverSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		
		}
	}
}
