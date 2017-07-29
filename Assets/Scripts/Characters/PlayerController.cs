using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Character controlledCharacter { get; private set; }
    public Light characterLight;

    private Character m_characterToDelete = null;

    public Light getCharacterLight()
    {
        return characterLight;
    }

    public void unpossesCurrentCharacter()
	{
		controlledCharacter.SetIsPossessed (false);

        if (controlledCharacter.GetCharacterType() == Character.CharacterType.Soul)
        {
            m_characterToDelete = controlledCharacter;
            m_characterToDelete.gameObject.SetActive(false);
        }

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

        if (m_characterToDelete != null)
        {
            DestroyImmediate(m_characterToDelete.gameObject);
            m_characterToDelete = null;
        }
    }

}
