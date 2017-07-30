using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

	public Character controlledCharacter { get; private set; }
    public Light characterLight;
    public GameObject m_deadParticles;

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
        if (character == CharactersManager.Instance.getTargetCharacter())
        {
            GameManager.Instance.EndGame(true);
        }
			
        // remove old character
        Character previousCharacter = TeleportManager.Instance.GetPreviousCharacter();
		if(previousCharacter != null && m_characterToDelete ==null)
            StartCoroutine(RemoveCharacter(previousCharacter));
        
		controlledCharacter = character;
		character.SetIsPossessed (character.GetCharacterType() != Character.CharacterType.Soul);

		CameraController.Instance.SetPlayerTarget(character.transform);

		characterLight.transform.parent = character.transform;
        controlledCharacter.m_characterVision.Init(characterLight);

        if (m_characterToDelete != null)
        {
			GameManager.Instance.GameStarted = true;
            DestroyImmediate(m_characterToDelete.gameObject);
            m_characterToDelete = null;
        }
    }

    // Display animations and call CharactersManager::RemoveCharacter after certain delay
    private IEnumerator RemoveCharacter(Character character)
    {
		character.Dead = true;

		CharacterIAMovement movement = character.GetComponent<CharacterIAMovement> ();
		if (movement) 
		{
			movement.SetEnabled (false);
		}
        // display animations
        yield return new WaitForSeconds(3f);

		CharactersManager.Instance.RemoveCharacter(character);
		GameObject particles = Instantiate(m_deadParticles, character.transform.position, character.transform.rotation);
		StartCoroutine("DestroyParticles", particles);
        // remove old character
        
    }

    private IEnumerator DestroyParticles(GameObject particles)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(particles.gameObject);
    }

}
