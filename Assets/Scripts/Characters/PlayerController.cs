using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

	public Character controlledCharacter { get; private set; }
    public Light characterLight;

    private Character m_characterToDelete = null;

    private void Awake()
    {
        characterLight.intensity = 0.0f;
    }

    public void TurnOnLights()
    {
        characterLight.DOIntensity(22.0f, 1.0f).SetDelay(1.0f);
    }

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
		character.SetIsPossessed (character.GetCharacterType() != Character.CharacterType.Soul);

		CameraController.Instance.SetPlayerTarget(character.transform);

		characterLight.transform.parent = character.transform;
        controlledCharacter.m_characterVision.Init(characterLight);

        if (m_characterToDelete != null)
        {
            DestroyImmediate(m_characterToDelete.gameObject);
            m_characterToDelete = null;
        }
    }

}
