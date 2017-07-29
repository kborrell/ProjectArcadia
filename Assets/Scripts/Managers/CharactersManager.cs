using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : SingletonMonoBehaviour<UIManager>
{
	private List<Character> mapCharacters = new List<Character>();

	private PlayerController playerController = new PlayerController();

	private Character targetCharacter = new Character ();

	public List<Object> characterPrefabs = new List<Object>();

	public void Initialize()
	{

	}
}
