using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : SingletonMonoBehaviour<CharactersManager>
{
	private List<Character> mapCharacters = new List<Character>();

	private PlayerController playerController = new PlayerController();

	private Character targetCharacter = new Character ();

	public List<GameObject> characterPrefabs = new List<GameObject>();

	public void Initialize()
	{
		Debug.Log ("Characters Manager");
        Character ch = Instantiate(characterPrefabs[0]).GetComponent<Character>() as Character;
        Character ch0 = Instantiate(characterPrefabs[0]).GetComponent<Character>() as Character;
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
}
