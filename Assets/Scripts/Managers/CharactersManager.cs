using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : SingletonMonoBehaviour<CharactersManager>
{
	private List<Character> mapCharacters = new List<Character>();

	public PlayerController playerController;

	private Character targetCharacter;

	public List<GameObject> characterPrefabs = new List<GameObject>();

	public List<GameObject> spawnPoints = new List<GameObject> ();
	int m_lastSpawnUsed = 0;

	public GameObject targetPrefab;
	public GameObject soulPrefab;
	public GameObject fistCharacterPrefab;

	float deltaSpawn;
	public float spawnFrequence = 5;
	public float enemiesOnMap = 10;

	public void Initialize()
	{
		deltaSpawn = spawnFrequence;

		Debug.Log ("Characters Manager");
		Character ch0 = Instantiate(fistCharacterPrefab).GetComponent<Character>() as Character;
        ch0.transform.position = new Vector3(0, 0, -6.59f);
		mapCharacters.Add (ch0);

		Character ch = Instantiate(soulPrefab).GetComponent<Character>() as Character;
        playerController.possesCharacter(ch);

		targetCharacter = Instantiate (targetPrefab).GetComponent<Character> () as Character;
        PlaceTarget(targetCharacter.gameObject);
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
				while (mapCharacters.Count < enemiesOnMap) 
				{
					int type = Random.Range (0, characterPrefabs.Count);
					Character ch = Instantiate(characterPrefabs[type], spawnPoints [m_lastSpawnUsed].transform.position, Quaternion.Euler(new Vector3(45.0f, 0.0f, 0.0f))).GetComponent<Character>() as Character;
					ch.m_characterIAMovement.SetOrigin(ch.transform.position);
					m_lastSpawnUsed++;
					m_lastSpawnUsed %= spawnPoints.Count;
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

    private void PlaceTarget(GameObject target)
    {
        int spawn = Random.Range(1, spawnPoints.Count-1);
        target.transform.position = spawnPoints[spawn].transform.position;
        target.GetComponent<Character>().m_characterIAMovement.SetOrigin(target.transform.position);
    }
}
