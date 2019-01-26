using UnityEngine;
using System.Collections;

public class PlayerSpawer : MonoBehaviour
{
    public GameObject PlayerPrefab;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public PlayerBase CreatePlayer()
    {
        return (Instantiate(PlayerPrefab) as GameObject).GetComponent<PlayerBase>();
    }
}
