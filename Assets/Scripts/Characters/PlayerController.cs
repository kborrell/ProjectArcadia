using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Character controlledCharacter { get; private set; }

	public Light characterLight;

	public void unpossesCurrentCharacter()
	{
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
        characterLight.transform.localPosition = new Vector3(-0.1f, 3.25f, -3.8f);
    }

}
