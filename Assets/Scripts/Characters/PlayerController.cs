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
        characterLight.DOIntensity(50.0f, 1.0f).SetDelay(1.0f);
    }

    public void TurnOffLights()
    {
        characterLight.DOIntensity(0.0f, 1.0f);
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
        // remove old character
        Character previousCharacter = TeleportManager.Instance.GetPreviousCharacter();
        if(previousCharacter != null)
            StartCoroutine(RemoveCharacter(previousCharacter));
        
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

    // Display animations and call CharactersManager::RemoveCharacter after certain delay
    private IEnumerator RemoveCharacter(Character character)
    {
        CharacterIAMovement iaMovement = character.GetComponent<CharacterIAMovement>();
        if(iaMovement != null) iaMovement.SetEnabled(false);
        // display animations
        yield return new WaitForSeconds(3f);
        // remove old character
        CharactersManager.Instance.RemoveCharacter(character);
    }

}
