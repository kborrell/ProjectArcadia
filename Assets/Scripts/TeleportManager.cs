using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : SingletonMonoBehaviour<TeleportManager> 
{
    private float m_maxTeleportZone;
    private bool m_changingSoul;
    [SerializeField] private GameObject m_soulParticle;
    private float m_soulChangeDuration;
    private float m_soulSpeed;

	private Character m_objetiveCharacter;

	public void Initialize()
    {
        m_maxTeleportZone = 10f;
        m_soulSpeed = 10f;
    }

	void Update () 
    {
        if (!m_changingSoul)
            HandleMouseInput();
        else
            DisplaySoulChange();
	}

	Character getCurrentCharacter()
	{
		return CharactersManager.Instance.getPlayerController ().controlledCharacter;
	}

    void HandleMouseInput()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(mouseRay, out hit))
        {
			if(Input.GetMouseButtonDown(0) && hit.transform.tag == "Character" && Vector3.Distance(getCurrentCharacter().transform.position, hit.transform.position) <= m_maxTeleportZone)
            {
				Character character = hit.transform.gameObject.GetComponent<Character> ();
				ChangeSoul(character);
            }
        }
    }

	void ChangeSoul(Character character)
    {
		if(getCurrentCharacter() != character)
        {
            m_soulParticle.transform.position = getCurrentCharacter().transform.position;
            m_objetiveCharacter = character;
            m_changingSoul = true;



            Debug.Log("Teleported to " + character.name);
		}
    }

    void DisplaySoulChange()
    {
        m_soulParticle.transform.position = Vector3.MoveTowards(m_soulParticle.transform.position, getCurrentCharacter().transform.position, Time.deltaTime * m_soulSpeed);

        if (Vector3.Distance(m_soulParticle.transform.position, getCurrentCharacter().transform.position) <= 0.5f)
        {
            m_changingSoul = false;
            CharactersManager.Instance.getPlayerController().unpossesCurrentCharacter();
            CharactersManager.Instance.getPlayerController ().possesCharacter(m_objetiveCharacter);
			m_objetiveCharacter = null;
            
		}
    }
}
