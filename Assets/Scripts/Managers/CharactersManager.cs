using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : SingletonMonoBehaviour<CharactersManager>
{
	private List<Character> mapCharacters = new List<Character>();

	private PlayerController playerController = new PlayerController();

	private Character targetCharacter = new Character ();

	public Character initialCharacter;

	public List<Object> characterPrefabs = new List<Object>();

	public void Initialize()
	{
		Debug.Log ("Characters Manager");
		playerController.possesCharacter (initialCharacter);
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
