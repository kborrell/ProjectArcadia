﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Character controlledCharacter { get; private set; }

	public Light characterLight;

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
		controlledCharacter = character;
		character.SetIsPossessed (true);

		CameraController.Instance.SetPlayerTarget(character.transform);

		characterLight.transform.parent = character.transform;
		characterLight.range = controlledCharacter.m_characterVision.GetVisionRange ();




    }

}
