using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Character controlledCharacter { get; private set; }

	public void unpossesCurrentCharacter()
	{
		if (controlledCharacter != null) 
		{
			controlledCharacter.SetIsPossessed (false);
		}
		controlledCharacter.SetIsPossessed (false);
		controlledCharacter = null;
	}

	public void possesCharacter(Character character)
	{
		CameraController.Instance.SetPlayerTarget(character.transform);
		character.SetIsPossessed (true);

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
