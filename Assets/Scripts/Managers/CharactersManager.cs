using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : SingletonMonoBehaviour<CharactersManager>
{
	private List<Character> mapCharacters = new List<Character>();

	public PlayerController playerController;

	private Character targetCharacter;

	public List<GameObject> characterPrefabs = new List<GameObject>();

	float deltaSpawn;
	public float spawnFrequence = 5;
	public float enemiesOnMap = 10;
	public float removeDistance = 100;
	public float spawnDistance = 20;
	public void Initialize()
	{
		deltaSpawn = spawnFrequence;

		Debug.Log ("Characters Manager");
		Character ch0 = Instantiate(characterPrefabs[1]).GetComponent<Character>() as Character;
        ch0.transform.position = new Vector3(0.0f, 0.0f, -5.59f);
		mapCharacters.Add (ch0);

		Character ch = Instantiate(characterPrefabs[0]).GetComponent<Character>() as Character;
        playerController.possesCharacter(ch);
    }

	public PlayerController getPlayerController()
	{
		return playerController;
	}

	public Character getTargetCharacter()
	{
		return targetCharacter;
	}

	void Update()
	{
		deltaSpawn += Time.deltaTime;

		if (deltaSpawn > spawnFrequence) 
		{
			for (int i = mapCharacters.Count - 1; i >= 0; i--) 
			{
				Character dest = mapCharacters [i];

				if (Vector3.Distance (dest.transform.position, playerController.controlledCharacter.transform.position) > removeDistance) 
				{
					GameObject.Destroy (dest.gameObject);
					mapCharacters.RemoveAt (i);
				}
			}

			while (mapCharacters.Count < enemiesOnMap) 
			{
				int type = Random.Range (2, characterPrefabs.Count);
				Character ch = Instantiate(characterPrefabs[type]).GetComponent<Character>() as Character;
				ch.transform.position = new Vector3 (Random.Range (-spawnDistance, spawnDistance), 0, Random.Range (-spawnDistance, spawnDistance));
				mapCharacters.Add (ch);
			}
		}
	}
}
