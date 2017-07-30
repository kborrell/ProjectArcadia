﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class TeleportManager : SingletonMonoBehaviour<TeleportManager>
{
    private float m_maxTeleportZone;
    private bool m_changingSoul;
    [SerializeField] private GameObject m_soulParticle;
    private float m_soulChangeDuration;
    [SerializeField] private float m_soulSpeed;

    private Character m_previousCharacter;
	private Character m_objetiveCharacter;
    private GameObject m_particles;

	public void Initialize()
    {
        m_maxTeleportZone = 10f;
    }

	public void ChangeSoul(Character character)
	{
		if (getCurrentCharacter() != character && !character.HasBeenPossessed())
		{
			m_particles = GameObject.Instantiate(m_soulParticle, getCurrentCharacter().transform.position, Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f)));

			m_objetiveCharacter = character;
            m_previousCharacter = getCurrentCharacter();
            m_changingSoul = true;

            m_previousCharacter.SetCharacterMovementEnabled(false);
            CharactersManager.Instance.getPlayerController().getCharacterLight().transform.parent = m_particles.transform;
            CameraController.Instance.SetPlayerTarget(m_particles.transform);
            CharactersManager.Instance.getPlayerController().unpossesCurrentCharacter();
            DisplaySoulChange();

			Debug.Log("Teleported to " + character.name);
		}
	}

    public Character GetPreviousCharacter()
    {
        return m_previousCharacter;
    }

	void Update () 
    {
        if (m_changingSoul)
            DisplaySoulChange();
        else
            HandleMouseInput();
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

    void DisplaySoulChange()
    {
        m_particles.transform.position = Vector3.MoveTowards(m_particles.transform.position, m_objetiveCharacter.transform.position, Time.deltaTime * m_soulSpeed);
        m_particles.transform.LookAt(m_objetiveCharacter.transform.position);

        if (Vector3.Distance(m_particles.transform.position, m_objetiveCharacter.transform.position) <= 0.5f)
        {
            m_changingSoul = false;
            m_previousCharacter.SetCharacterMovementEnabled(true);
            CharactersManager.Instance.getPlayerController ().possesCharacter(m_objetiveCharacter);
			m_objetiveCharacter = null;
            m_previousCharacter = null;

			DestroyImmediate(m_particles.gameObject);
			m_particles = null;
		}
    }
}
