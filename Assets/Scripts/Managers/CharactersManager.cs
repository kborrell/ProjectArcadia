using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : SingletonMonoBehaviour<CharactersManager>
{
	private List<Character> mapCharacters = new List<Character>();

	public PlayerController playerController;

	private Character targetCharacter;

	public List<GameObject> characterPrefabs = new List<GameObject>();

	public GameObject targetPrefab;
	public GameObject soulPrefab;
	public GameObject fistCharacterPrefab;

	float deltaSpawn;
	public float spawnFrequence = 5;
	public float enemiesOnMap = 10;
	public float removeDistance = 100;
	public float spawnDistance = 20;
	public void Initialize()
	{
		deltaSpawn = spawnFrequence;

		Debug.Log ("Characters Manager");
		Character ch0 = Instantiate(fistCharacterPrefab).GetComponent<Character>() as Character;
        ch0.transform.position = new Vector3(0.0f, 0.09f, -6.59f);
		mapCharacters.Add (ch0);

		Character ch = Instantiate(soulPrefab).GetComponent<Character>() as Character;
        playerController.possesCharacter(ch);

		targetCharacter = Instantiate (targetPrefab).GetComponent<Character> () as Character;
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

		Character controlledCharacter = playerController.controlledCharacter;

		if (controlledCharacter != null && GameManager.Instance.GameStarted) 
		{
			if (deltaSpawn > spawnFrequence) 
			{
				for (int i = mapCharacters.Count - 1; i >= 0; i--) 
				{
					Character dest = mapCharacters [i];

					if (Vector3.Distance (dest.transform.position, controlledCharacter.transform.position) > removeDistance) 
					{
						GameObject.Destroy (dest.gameObject);
						mapCharacters.RemoveAt (i);
					}
				}

				while (mapCharacters.Count < enemiesOnMap) 
				{
					int type = Random.Range (0, characterPrefabs.Count);
					Character ch = Instantiate(characterPrefabs[type]).GetComponent<Character>() as Character;
					ch.transform.position = controlledCharacter.transform.position + new Vector3 (Random.Range (-spawnDistance, spawnDistance), 0, Random.Range (-spawnDistance, spawnDistance));
					mapCharacters.Add (ch);
				}
			}
		}

	}

    public void RemoveCharacter(Character character)
    {
        Debug.Log("Removing character");
        mapCharacters.Remove(character);
        GameObject.Destroy(character.gameObject);
    }
}
